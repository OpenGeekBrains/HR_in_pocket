using System;

namespace HRInPocket.Infrastructure.Models.Records.Assignments
{
    public abstract record Assignment(string place_name)
    {
        public long id { get; set; }
        public Guid applicant_id { get; private set; }
        public DateTime date_added { get; } = DateTime.Now;
        public int number_of_responses { get; set; }
        public int number_of_invitations { get; set; }

        public void AssignApplicant(Guid applicantId) => applicant_id = applicantId;
    }
}