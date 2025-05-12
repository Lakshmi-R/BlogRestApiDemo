namespace BlogRestApiDemo
{
    public class ApiResponse<T>
    {
        public string Message { get; set; } = string.Empty;

        public bool Success { get; set; }   
        public T? Data { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message="Request successful")
            => new ApiResponse<T> { Message = message, Success = true , Data = data};

        public static ApiResponse<T> FailureResponse(string message)
            => new ApiResponse<T> { Message = message, Success=false };
    }
}
