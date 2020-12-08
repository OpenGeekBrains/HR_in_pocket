using System;

namespace HRInPocket.Infrastructure.Models.Records
{
    public record FeedbackRequest(string name, string email, string phone_number, string message)
    {
        public long id { get; set; }
        public Guid AssignedApplicant { get; set; }
        public DateTime RequestCreation { get; set; }
        public DateTime ApplicantAssignedDate { get; set; }
    }
}