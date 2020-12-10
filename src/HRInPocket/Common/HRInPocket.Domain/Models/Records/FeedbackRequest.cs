using System;

namespace HRInPocket.Domain.Models.Records
{
    public class FeedbackRequest
    {
        public long id { get; set; }

        public FeedbackRequest(string name, string email, string phoneNumber, string message)
        {
            this.name = name;
            this.email = email;
            this.phone_number = phoneNumber;
            this.message = message;
        }
        
        public readonly string name;
        public readonly string email;
        public readonly string phone_number;
        public readonly string message;
        public readonly DateTime RequestCreation = DateTime.Now;
        
        public Guid AssignedApplicant { get; set; }
        public DateTime ApplicantAssignedDate { get; set; }
    }
}