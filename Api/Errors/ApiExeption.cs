namespace Api.Errors
{
    public class ApiExeption
    {
        public ApiExeption(int statusCode, string messages=null, string details=null)
        {
            this.statusCode = statusCode;
            this.Messages = messages;
            this.Details = details;
        }

        public int statusCode { get; set; }
        public string Messages { get; set; }
        public string Details { get; set; }
    }
}