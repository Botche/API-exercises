namespace LibraryAPI.Common.Exceptions
{
	using System;

	public class UnauthorizedAccessCustomException : CustomException
	{
		public UnauthorizedAccessCustomException(string message) 
			: base(message)
		{
		}

		public UnauthorizedAccessCustomException(string message, Exception innerException) 
			: base(message, innerException)
		{
		}
	}
}
