using System;
using System.Linq;

using HRInPocket.Infrastructure.Models;
using HRInPocket.Infrastructure.Models.JsonReturnModels;
using HRInPocket.Infrastructure.Models.Records;
using HRInPocket.Infrastructure.Models.Records.Assignments;
using HRInPocket.Infrastructure.Services;
using HRInPocket.Interfaces;

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


        public AssignmentsApiControllers(ILogger<AssignmentsApiControllers> logger)
        {
            _logger = logger;
            var authService = new AuthService();
            _applicantsManager = new ApplicantManagerService(authService);
            _assignmentsManager = new AssignmentsManagerService(_applicantsManager);

            logger.LogDebug(LogEvents.GenerateItems, "Generate test applicants and assignments");
            
            var applicant1 = new Applicant(new UserData("TestApplicant1@gmail.com", "test1"));
            var applicant2 = new Applicant(new UserData("TestApplicant2@yahoo.net", "test2"));
            _applicantsManager.Register(applicant1);
            _applicantsManager.Register(applicant2);
            _assignmentsManager.Add(new InvitationAssignment("Invitation 1"));
            _assignmentsManager.Add(new InvitationAssignment("Invitation 2") {number_of_invitations = 2, number_of_responses = 1});
            _assignmentsManager.Add(new InvitationAssignment("Invitation 3"));
            _assignmentsManager.Add(new CoveringAssignment("Covering 1"));

            var resume1 = new ResumeAssignment("Resume 1");
            resume1.AssignApplicant(applicant1.Id);
            _assignmentsManager.Add(resume1);
            var resume2 = new ResumeAssignment("Resume 2");
            resume2.AssignApplicant(applicant1.Id);
            _assignmentsManager.Add(resume2);
            var resume3 = new ResumeAssignment("Resume 3") {number_of_invitations = 3, number_of_responses = 4};
            resume3.AssignApplicant(applicant2.Id);
            _assignmentsManager.Add(resume3);
        }


        #region Get All

        [HttpGet("all")]
        public IActionResult Get() => new JsonResult(new ArrayContent(_assignmentsManager.Get(), true));

        [HttpGet("{applicantId}")]
        public IActionResult Get(Guid applicantId) => Get(applicantId, null);

        [HttpGet("{applicantId}/{type}")]
        public IActionResult Get(Guid applicantId, AssignmentType? type)
        {
            if (!_applicantsManager.IsApplicantExist(applicantId)) return BadRequest();

            var content = _assignmentsManager.GetAssignmentsOfType(applicantId, type);

            return !(content ?? Array.Empty<Assignment>()).Any()
                ? NotFound()
                : new JsonResult(new ArrayContent(content, true));
        }

        #endregion

        #region Get One

        [HttpGet("{applicantId}/{type}/{assignmentId}")]
        public IActionResult GetById(Guid applicantId, AssignmentType type, long assignmentId)
        {
            if (!_applicantsManager.IsApplicantExist(applicantId)) return BadRequest();

            var assignments = _assignmentsManager.GetAssignmentsOfType(applicantId, type);

            var assignment = assignments.FirstOrDefault(a => a.id == assignmentId);
            return assignment is null
                ? NotFound()
                : new JsonResult(new { content = assignment, result = true });
        }

        #endregion

        #region Create

        [HttpPost("{applicantId}/Invitation")]
        public IActionResult Create(Guid applicantId, [FromBody] InvitationAssignment invitation) => CreateAssignment(applicantId, invitation);

        [HttpPost("{applicantId}/Resume")]
        public IActionResult Create(Guid applicantId, [FromBody] ResumeAssignment resume) => CreateAssignment(applicantId, resume);

        [HttpPost("{applicantId}/Covering")]
        public IActionResult Create(Guid applicantId, [FromBody] CoveringAssignment covering) => CreateAssignment(applicantId, covering);

        #endregion

        #region Update

        [HttpPut("{applicantId}/Invitation")]
        public IActionResult UpdateInvitation(Guid applicantId, [FromBody] InvitationAssignment invitation) => UpdateAssignment(applicantId, invitation);

        [HttpPut("{applicantId}/Resume")]
        public IActionResult UpdateResume(Guid applicantId, [FromBody] ResumeAssignment resume) => UpdateAssignment(applicantId, resume);

        [HttpPut("{applicantId}/Covering")]
        public IActionResult UpdateCovering(Guid applicantId, [FromBody] CoveringAssignment covering) => UpdateAssignment(applicantId, covering);

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
        private IActionResult CreateAssignment(Guid applicantId, Assignment assignment)
        {
            if(!_applicantsManager.IsApplicantExist(applicantId)) return  BadRequest();
            
            var registeredAssignment = _assignmentsManager.Add(assignment);
            
            _assignmentsManager.AssignApplicant(registeredAssignment, applicantId);
            
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
            if (!_applicantsManager.IsApplicantExist(applicantId)) return BadRequest();

            if(!_assignmentsManager.Update(assignment))
                return NotFound();
            
            return Ok();
        }
        
        #endregion
    }
}
