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
        
        public readonly string place_name;
        public readonly DateTime date_added = DateTime.Now;
        
        public Guid applicant_id { get; private set; }
        public int number_of_responses { get; set; }
        public int number_of_invitations { get; set; }

        public void AssignApplicant(Guid applicantId) => applicant_id = applicantId;
    }
}