namespace HRInPocket.Infrastructure.Models.Records.Assignments
{
    public record FeedbackAssignment(string place_name, FeedbackRequest request) : Assignment(place_name)
    {
        
    }
}