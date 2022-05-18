namespace API.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string? Detail { get; set; }
        public CodeErrorException(int statusCode, string? errorMessage = null, string? detail = null) : base(statusCode, errorMessage)
        {
            Detail = detail;
        }
    }
}
