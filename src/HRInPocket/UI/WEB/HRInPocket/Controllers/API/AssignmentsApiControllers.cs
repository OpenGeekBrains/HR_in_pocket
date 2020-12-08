using System;
using System.Collections.Generic;
using System.Linq;

using HRInPocket.Infrastructure.Models;
using HRInPocket.Infrastructure.Models.Assignments;
using HRInPocket.Infrastructure.Models.JsonReturnModels;
using HRInPocket.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace HRInPocket.Controllers.API
{
    // todo: Rework to work with DB
    [Route(WebAPI.Assignments)]
    [ApiController]
    public class AssignmentsApiControllers : ControllerBase
    {
        private static List<Assignment> _assignments;
        private static long _counter;
        private static long Counter => ++_counter;

        static AssignmentsApiControllers()
        {
            var applicantId1 = Guid.NewGuid();
            var applicantId2 = Guid.NewGuid();
            _assignments = new List<Assignment>
            {
                new InvitationAssignment(Counter, "Invitation 1", applicantId1),
                new InvitationAssignment(Counter, "Invitation 2", applicantId1) {number_of_invitations = 2, number_of_responses = 1},
                new InvitationAssignment(Counter, "Invitation 3", applicantId2),
                new CoveringAssignment(Counter, "Covering 1", applicantId1),
                new CoveringAssignment(Counter, "Covering 2", applicantId2) {number_of_invitations = 7, number_of_responses = 2},
                new CoveringAssignment(Counter, "Covering 3", applicantId2),
                new CoveringAssignment(Counter, "Covering 4", applicantId2) {number_of_invitations = 4, number_of_responses = 1},
                new ResumeAssignment(Counter, "Resume 1", applicantId1),
                new ResumeAssignment(Counter, "Resume 2", applicantId1),
                new ResumeAssignment(Counter, "Resume 3", applicantId2) {number_of_invitations = 3, number_of_responses = 4},
                new ResumeAssignment(Counter, "Resume 4", applicantId1),
                new ResumeAssignment(Counter, "Resume 5", applicantId2) {number_of_invitations = 5, number_of_responses = 2},
            };

        }


        #region Get All

        [HttpGet("all")]
        public IActionResult Get() => new JsonResult(new ArrayContent(_assignments, true));

        [HttpGet("{applicantId}")]
        public IActionResult Get(Guid applicantId) => Get(applicantId, null);

        [HttpGet("{applicantId}/{type}")]
        public IActionResult Get(Guid applicantId, AssignmentType? type)
        {
            if (!CheckApplicant(applicantId)) return BadRequest();

            var content = GetAssignmentsOfType(applicantId, type);

            return !(content ?? Array.Empty<Assignment>()).Any()
                ? NotFound()
                : new JsonResult(new ArrayContent(content, true));
        }

        #endregion

        #region Get One

        [HttpGet("{applicantId}/{type}/{assignmentId}")]
        public IActionResult GetById(Guid applicantId, AssignmentType type, long assignmentId)
        {
            if (!CheckApplicant(applicantId)) return BadRequest();

            var assignments = GetAssignmentsOfType(applicantId, type);

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

        [HttpPut("{applicantId}/Invitation/{assignmentId}")]
        public IActionResult UpdateInvitation(Guid applicantId, long assignmentId, [FromBody] InvitationAssignment invitation) => UpdateAssignment(applicantId, assignmentId, invitation);

        [HttpPut("{applicantId}/Resume/{assignmentId}")]
        public IActionResult UpdateResume(Guid applicantId, long assignmentId, [FromBody] ResumeAssignment resume) => UpdateAssignment(applicantId, assignmentId, resume);

        [HttpPut("{applicantId}/Covering/{assignmentId}")]
        public IActionResult UpdateCovering(Guid applicantId, long assignmentId, [FromBody] CoveringAssignment covering) => UpdateAssignment(applicantId, assignmentId, covering);

        #endregion


        #region Methods

        #region Api Cut

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
        private IActionResult CreateAssignment(Guid applicantId, Assignment assignment) =>
            !CheckApplicant(applicantId)
                ? BadRequest()
                : new JsonResult(AddAssignment(assignment, applicantId).id);

        /// <summary>
        /// Update record in repository
        /// </summary>
        /// <param name="applicantId">Applicant Id</param>
        /// <param name="assignmentId">Id of assignment, that must be updated</param>
        /// <param name="assignment">Record data to update</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Status code 400, if applicant with <paramref name="applicantId"/> not found</item>
        /// <item>Status code 404, if assignment with <paramref name="assignmentId"/> not found</item>
        /// <item>Status code 200, if assignment fount and updated </item>
        /// </list>
        /// </returns>
        private IActionResult UpdateAssignment(Guid applicantId, long assignmentId, Assignment assignment)
        {
            if (!CheckApplicant(applicantId)) return BadRequest();

            var covering = _assignments.Where(a => a.applicant_id == applicantId).OfType<CoveringAssignment>().FirstOrDefault(a => a.id == assignmentId);
            if (covering is null) return NotFound();

            var index = _assignments.IndexOf(covering);
            _assignments[index] = assignment;

            return Ok();
        }

        #endregion

        #region Helpers

        private static bool CheckApplicant(Guid id)
        {
            if (id == Guid.Empty) return false;
            // todo: check if applicant not exist
            return true;
        }

        private static Assignment AddAssignment(Assignment assignment, Guid applicantId)
        {
            assignment.id = Counter;
            assignment.AssignApplicant(applicantId);
            _assignments.Add(assignment);
            return assignment;
        }


        private static IEnumerable<Assignment> GetAssignmentsOfType(Guid applicantId, AssignmentType? type = null)
        {
            var content = _assignments.Where(a => a.applicant_id == applicantId);

            if (type is not null)
                content = type switch
                {
                    AssignmentType.Invitation => content.OfType<InvitationAssignment>(),
                    AssignmentType.Resume     => content.OfType<ResumeAssignment>(),
                    AssignmentType.Covering   => content.OfType<CoveringAssignment>(),
                    _                         => null
                };

            return content;
        }

        #endregion

        #endregion
    }
}
