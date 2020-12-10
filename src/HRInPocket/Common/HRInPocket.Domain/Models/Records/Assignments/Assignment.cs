using System;

namespace HRInPocket.Domain.Models.Records.Assignments
{
    public abstract class Assignment
    {
        public long id { get; set; }

        public Assignment(string placeName)
        {
            place_name = placeName;
        }
        
        public string place_name { get; }
        public DateTime date_added { get; } = DateTime.Now;
        
        public Guid applicant_id { get; private set; }
        public int number_of_responses { get; set; }
        public int number_of_invitations { get; set; }

        public void AssignApplicant(Guid applicantId) => applicant_id = applicantId;
    }
}