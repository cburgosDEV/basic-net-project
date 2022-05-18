namespace API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public CodeErrorResponse(int statusCode, string? errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetDefaultMessageStatusCode(statusCode);
        }
        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Request has errors ",
                401 => "Not authorized for this resource",
                404 => "Resource not found",
                500 => "There was some errors in server",
                _ => string.Empty,
            };
        }
    }
}
