using System;
using System.Linq;

using HRInPocket.Domain;
using HRInPocket.Domain.Models.JsonReturnModels;
using HRInPocket.Domain.Models.Records.Assignments;
using HRInPocket.Infrastructure.Models;
using HRInPocket.Interfaces;
using HRInPocket.Services.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HRInPocket.Controllers.API
{
    // todo: Rework to work with DB
    [Route(WebAPI.Assignments)]
    [ApiController]
    public class AssignmentsApiControllers : ControllerBase
    {
        private readonly ILogger<AssignmentsApiControllers> _logger;
        private readonly AssignmentsManagerService _assignmentsManager;
        private readonly ApplicantManagerService _applicantsManager;


        public AssignmentsApiControllers(ILogger<AssignmentsApiControllers> logger, ApplicantManagerService applicantManager, AssignmentsManagerService assignmentsManager)
        {
            _logger = logger;
            _applicantsManager = applicantManager;
            _assignmentsManager = assignmentsManager;
        }

        #region Get All

        [HttpGet("all")]
        public IActionResult Get()
        {
            _logger.LogInformation(LogEvents.ListItems,"Get all Assignments");
            return new JsonResult(new ArrayContent(_assignmentsManager.Get(), true));
        }

        [HttpGet("{applicantId}")]
        public IActionResult Get(Guid applicantId) => Get(applicantId, null);

        [HttpGet("{applicantId}/{type}")]
        public IActionResult Get(Guid applicantId, AssignmentType? type)
        {
            _logger.LogInformation(LogEvents.ListItems,$"Get{(type is null ? " all " : $" {type} ")}Assignments of Applicant {applicantId}");
            if (!_applicantsManager.IsApplicantExist(applicantId))
            {
                _logger.LogWarning(LogEvents.ListItemsNotFound, $"Can't return Assignments, Applicant {applicantId} not found");
                return BadRequest();
            }

            var content = _assignmentsManager.GetAssignmentsOfType(applicantId, type);

            if (!(content ?? Array.Empty<Assignment>()).Any())
            {
                _logger.LogWarning(LogEvents.ListItemsNotFound, $"Assignments{(type is null ? " " : $" of type {type} ")}not found for Applicant {applicantId}");
                return NotFound();
            }

            _logger.LogInformation(LogEvents.ListItems,$"Successfully find assignments{(type is null ? " " : $" of type {type} ")}for Applicant {applicantId}");
            return new JsonResult(new ArrayContent(content, true));
        }

        #endregion

        #region Get One

        [HttpGet("{applicantId}/{type}/{assignmentId}")]
        public IActionResult GetById(Guid applicantId, AssignmentType type, long assignmentId)
        {
            _logger.LogInformation(LogEvents.GetItem, $"Get {type} Assignments of Applicant {applicantId}");
            if (!_applicantsManager.IsApplicantExist(applicantId))
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, $"Can't return Assignment {assignmentId}, Applicant {applicantId} not found");
                return BadRequest();
            }

            var assignments = _assignmentsManager.GetAssignmentsOfType(applicantId, type);

            var assignment = assignments.FirstOrDefault(a => a.id == assignmentId);
            if (assignment is null)
            {
                _logger.LogWarning(LogEvents.ListItemsNotFound, $"Assignment {assignmentId} not found for Applicant {applicantId}");
                return NotFound();
            }

            _logger.LogInformation(LogEvents.ListItems,$"Successfully find assignment {assignmentId} for Applicant {applicantId}");
            return new JsonResult(new {content = assignment, result = true});
        }

        #endregion

        #region Insert

        [HttpPost("{applicantId}/Invitation")]
        public IActionResult Insert(Guid applicantId, [FromBody] InvitationAssignment invitation)
        {
            _logger.LogInformation(LogEvents.InsertItem,$"Create invitation assignment for Applicant {applicantId}");
            return InsertAssignment(applicantId, invitation);
        }

        [HttpPost("{applicantId}/Resume")]
        public IActionResult Insert(Guid applicantId, [FromBody] ResumeAssignment resume)
        {
            _logger.LogInformation(LogEvents.InsertItem,$"Create resume assignment for Applicant {applicantId}");
            return InsertAssignment(applicantId, resume);
        }

        [HttpPost("{applicantId}/Covering")]
        public IActionResult Insert(Guid applicantId, [FromBody] CoveringAssignment covering)
        {
            _logger.LogInformation(LogEvents.InsertItem,$"Create covering assignment for Applicant {applicantId}");
            return InsertAssignment(applicantId, covering);
        }
        
        [HttpPost("{applicantId}/Feedback")]
        public IActionResult Insert(Guid applicantId, [FromBody] FeedbackAssignment feedback)
        {
            _logger.LogInformation(LogEvents.InsertItem,$"Create feedback assignment for Applicant {applicantId}");
            return InsertAssignment(applicantId, feedback);
        }

        #endregion

        #region Update

        [HttpPut("{applicantId}/Invitation")]
        public IActionResult UpdateInvitation(Guid applicantId, [FromBody] InvitationAssignment invitation)
        {
            _logger.LogInformation(LogEvents.InsertItem,$"Update invitation assignment for Applicant {applicantId}");
            return UpdateAssignment(applicantId, invitation);
        }

        [HttpPut("{applicantId}/Resume")]
        public IActionResult UpdateResume(Guid applicantId, [FromBody] ResumeAssignment resume)
        {
            _logger.LogInformation(LogEvents.InsertItem,$"Update resume assignment for Applicant {applicantId}");
            return UpdateAssignment(applicantId, resume);
        }

        [HttpPut("{applicantId}/Covering")]
        public IActionResult UpdateCovering(Guid applicantId, [FromBody] CoveringAssignment covering)
        {
            _logger.LogInformation(LogEvents.InsertItem,$"Update covering assignment for Applicant {applicantId}");
            return UpdateAssignment(applicantId, covering);
        }
        
        [HttpPut("{applicantId}/Feedback")]
        public IActionResult UpdateFeedback(Guid applicantId, [FromBody] FeedbackAssignment feedback)
        {
            _logger.LogInformation(LogEvents.InsertItem,$"Update feedback assignment for Applicant {applicantId}");
            return UpdateAssignment(applicantId, feedback);
        }

        #endregion

        #region Assign

        [HttpPut("assign/{assignmentId}/{applicantId}")]
        public IActionResult AssignAssignment(long assignmentId, Guid applicantId)
        {
            _logger.LogInformation(LogEvents.UpdateItem, $"Assign assignment {assignmentId} to Applicant {applicantId}");
            if (_applicantsManager.IsApplicantExist(applicantId))
            {
                _logger.LogWarning(LogEvents.InsertItemFailure, $"Cannot Assign assignment for Applicant {applicantId} that not exist");
                return BadRequest();
            }

            var assignment = _assignmentsManager.Get(assignmentId);
            if (assignment is null)
            {
                _logger.LogWarning(LogEvents.InsertItemFailure, $"Cannot Assign assignment {assignmentId} that not exist");
                return NotFound();
            }

            _logger.LogInformation(LogEvents.UpdateItem, $"Successfully Assign assignment {assignmentId} to Applicant {applicantId}");
            return Ok();
        }

        #endregion


        #region Methods

        /// <summary>
        /// Add record to repository
        /// </summary>
        /// <param name="applicantId">Applicant Id</param>
        /// <param name="assignment">Entry that must be saved</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Status code 400, if applicant with <paramref name="applicantId"/> not found</item>
        /// <item>Record identifier with type <see cref="long"/></item>
        /// </list>
        /// </returns>
        private IActionResult InsertAssignment(Guid applicantId, Assignment assignment)
        {
            if(!_applicantsManager.IsApplicantExist(applicantId))
            {
                _logger.LogWarning(LogEvents.InsertItemFailure, $"Cannot Insert assignment for Applicant {applicantId} that not exist");
                return BadRequest();
            }

            var registeredAssignment = _assignmentsManager.Add(assignment);
            
            _logger.LogInformation(LogEvents.InsertItem, $"Assign assignment {registeredAssignment.id} to Applicant {applicantId}");
            _assignmentsManager.AssignApplicant(registeredAssignment, applicantId);
            
            _logger.LogInformation(LogEvents.InsertItem, $"Assignment {registeredAssignment.id} successfully Inserted and Assigned to Applicant {applicantId}");
            return new JsonResult(registeredAssignment.id);
        }

        /// <summary>
        /// Update record in repository
        /// </summary>
        /// <param name="applicantId">Applicant Id</param>
        /// <param name="assignment">Record data to update</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Status code 400, if applicant with <paramref name="applicantId"/> not found</item>
        /// <item>Status code 404, if assignment with <paramref name="assignment"/> not found</item>
        /// <item>Status code 200, if assignment fount and updated </item>
        /// </list>
        /// </returns>
        private IActionResult UpdateAssignment(Guid applicantId, Assignment assignment)
        {
            if (!_applicantsManager.IsApplicantExist(applicantId))
            {
                _logger.LogWarning(LogEvents.InsertItemFailure, $"Cannot Update assignment for Applicant {applicantId} that not exist");
                return BadRequest();
            }

            if(!_assignmentsManager.Update(assignment))
            {
                _logger.LogWarning(LogEvents.InsertItemFailure, $"Cannot Update assignment that not exist");
                return NotFound();
            }

            _logger.LogInformation(LogEvents.InsertItem, $"Assignment {assignment.id} successfully Updated");
            return Ok();
        }
        
        #endregion
    }
}
