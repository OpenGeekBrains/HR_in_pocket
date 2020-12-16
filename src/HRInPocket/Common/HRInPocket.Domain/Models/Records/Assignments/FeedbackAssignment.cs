namespace HRInPocket.Domain.Models.Records.Assignments
{
    public class FeedbackAssignment : Assignment
    {
        public FeedbackAssignment(string place_name, FeedbackRequest request) : base(place_name)
        {
            Request = request;
        }

        public FeedbackRequest Request { get; }
    }
}