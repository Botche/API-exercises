namespace LibraryAPI.Common.Exceptions
{
	using System;

	public class EntityDoesNotExist : CustomException
	{
		public EntityDoesNotExist(string exceptionMessage)
				: base(exceptionMessage)
		{
		}

		public EntityDoesNotExist(string exceptionMessage, Exception inner)
				: base(exceptionMessage, inner)
		{
		}
	}
}
