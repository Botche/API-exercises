namespace LibraryAPI.Common.Exceptions
{
	using System;

	public class BookDoesNotExist : Exception
	{
		public BookDoesNotExist(string exceptionMessage)
				: base(exceptionMessage)
		{
		}

		public BookDoesNotExist(string exceptionMessage, Exception inner)
				: base(exceptionMessage, inner)
		{
		}
	}
}
