using System;

namespace HRInPocket.Infrastructure.Models.Records.Assignments
{
    public abstract record Assignment(long id, string place_name, Guid applicant_id)
    {
        public long id { get; set; } = id;
        public string place_name { get; init; } = place_name;
        public Guid applicant_id { get; private set; } = applicant_id;
        public DateTime date_added { get; } = DateTime.Now;
        public int number_of_responses { get; set; }
        public int number_of_invitations { get; set; }

        public void AssignApplicant(Guid applicantId) => applicant_id = applicantId;
    }
}