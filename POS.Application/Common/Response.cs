namespace POS.Application.Common
{
	public class Response<T>
	{
		public T Data { get; set; }
		public bool Success { get; set; }
		public string Message { get; set; }
		public IEnumerable<BaseError> Errors { get; set; }
    }
}
