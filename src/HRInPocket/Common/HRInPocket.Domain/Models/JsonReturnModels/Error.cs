namespace HRInPocket.Domain.Models.JsonReturnModels
{
    public struct Error
    {
        public Error(string error, bool result, string badParameter)
        {
            this.error = error;
            this.result = result;
            bad_parameter = badParameter;
        }
        
        public string error { get; set; }
        public bool result { get; set; }
        public string bad_parameter { get; set; }
    }
}