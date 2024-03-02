namespace POS.Application.Common.Exceptions
{
	public class ValidationCustomException : Exception
	{
        public ValidationCustomException() : base("One or more validation failures")
        {
            Errors = new List<BaseError>();
        }

        public ValidationCustomException(IEnumerable<BaseError> errors) : this()
        {
            Errors = errors;
        }

        public IEnumerable<BaseError> Errors { get; set; }
    }
}
