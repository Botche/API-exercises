namespace LibraryAPI.Common.Exceptions
{
	using System;

	public class EntityDoesNotExistException : CustomException
	{
		public EntityDoesNotExistException(string exceptionMessage)
				: base(exceptionMessage)
		{
		}

		public EntityDoesNotExistException(string exceptionMessage, Exception inner)
				: base(exceptionMessage, inner)
		{
		}
	}
}
