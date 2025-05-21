namespace Unab.Practice.Employees.Transversal
{
    public sealed class Response<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; }
        public List<BaseError>? Errors { get; set; } = [];
    }
}
