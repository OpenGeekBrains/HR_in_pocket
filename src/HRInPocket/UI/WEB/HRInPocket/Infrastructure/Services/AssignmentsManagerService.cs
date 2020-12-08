using System;
using System.Collections.Generic;
using System.Linq;

using HRInPocket.Infrastructure.Models;
using HRInPocket.Infrastructure.Models.Records.Assignments;

namespace HRInPocket.Infrastructure.Services
{
    public class AssignmentsManagerService
    {
        private readonly ApplicantManagerService _applicantManager;

        public AssignmentsManagerService(ApplicantManagerService applicantManager)
        {
            _applicantManager = applicantManager;
        }
        
        private static readonly List<Assignment> Assignments = new();
        #region Counter
        private static long _counter;
        private static long Counter => ++_counter; 
        #endregion



        #region Get

        public IEnumerable<Assignment> Get() => Assignments;

        public IEnumerable<Assignment> Get(Func<Assignment, bool> where) => Assignments.Where(where);

        public IEnumerable<TAssignment> Get<TAssignment>() where TAssignment : Assignment => Assignments.OfType<TAssignment>(); 
        
        public IEnumerable<Assignment> GetAssignmentsOfType(Guid applicantId, AssignmentType? type = null)
        {
            var content = Assignments.Where(a => a.applicant_id == applicantId);

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

        public Assignment Add(Assignment assignment)
        {
            assignment.id = Counter;
            Assignments.Add(assignment);
            return assignment;
        }

        public bool Update(Assignment assignment)
        {
            var covering = Assignments.Find(a=>a.id == assignment.id);
            if (covering is null) return false;

            var index = Assignments.IndexOf(covering);
            Assignments[index] = assignment;
            return true;
        }


        public void AssignApplicant(Assignment assignment, Guid applicantId) => assignment.AssignApplicant(applicantId);
    }
}