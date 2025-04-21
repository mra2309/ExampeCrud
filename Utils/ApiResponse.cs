
    public class ApiResponse<T>
    {
        public string Status { get; set; } = "success";
        public T? Data { get; set; }
        public string? Error { get; set; }
    }