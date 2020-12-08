namespace HRInPocket.Infrastructure.Models.Records
{
    public record Applicant(UserData UserData) : Account(UserData)
    {
    }
}