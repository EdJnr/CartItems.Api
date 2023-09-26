namespace CartItems.Api.Responses
{
    public class ApiResponse<T>
    {
        public bool Successful { get; set; }

        public int StatusCode { get; set; }

        public T Data { get; set; }

        public string? Error { get; set; }

        public ApiResponse(bool success, T data, int statusCode, string? error = null)
        {
            if(success)
            {
                Successful = true;
                Data = data;
                StatusCode = statusCode;
            }
            else
            {
                Successful = false;
                Error = error;
                StatusCode = statusCode;
            }
           
        }
    }
}
