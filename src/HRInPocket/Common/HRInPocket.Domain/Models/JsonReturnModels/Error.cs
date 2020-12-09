namespace HRInPocket.Domain.Models.JsonReturnModels
{
    public readonly struct Error
    {
        public Error(string error, bool result, string badParameter)
        {
            this.error = error;
            this.result = result;
            bad_parameter = badParameter;
        }
        
        public readonly string error;
        public readonly bool result;
        public readonly string bad_parameter;
    }
}