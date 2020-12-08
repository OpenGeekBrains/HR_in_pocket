namespace HRInPocket.Infrastructure.Models.JsonReturnModels
{
    public record Error(string error, bool result, string bad_parameter);
}