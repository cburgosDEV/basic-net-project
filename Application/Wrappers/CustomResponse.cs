namespace Application.Wrappers
{
    public class CustomResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
        public T? Data { get; set; }

        public CustomResponse()
        {

        }
        public CustomResponse(T data, string message = "")
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public CustomResponse(T data, List<string> errors, bool succeeded, string message = "")
        {
            Succeeded = succeeded;
            Message = message;
            Data = data;
            Errors = errors;
        }
        public CustomResponse(string message = "")
        {
            Succeeded = false;
            Message = message;
        }
    }
}
