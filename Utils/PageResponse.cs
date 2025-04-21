

    public class PageResponse<T>
    {
        public string Status {get; set; } = "success";
        public IEnumerable<T>? Data {get; set; }
        public Meta? Meta {get; set; }
        public string? Error {get; set; }
    }

    public class Meta
    {
        public int Page {get; set;}
        public int PageSize {get; set;}
        public int TotalItem {get; set;}
        public int TotalPages {get; set;}
    }