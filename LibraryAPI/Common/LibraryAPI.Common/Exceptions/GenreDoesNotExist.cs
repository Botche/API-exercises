namespace LibraryAPI.Common.Exceptions
{
	using System;

	public class GenreDoesNotExist : Exception
	{
		public GenreDoesNotExist(string exceptionMessage)
				: base(exceptionMessage)
		{
		}

		public GenreDoesNotExist(string exceptionMessage, Exception inner)
				: base(exceptionMessage, inner)
		{
		}
	}
}
