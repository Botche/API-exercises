namespace LibraryAPI.Common.Exceptions
{
	using System;
	using System.Collections.Generic;

	using Microsoft.AspNetCore.Mvc.ModelBinding;

	public class BulkEditModelException : CustomException
	{
		public BulkEditModelException(IEnumerable<ModelError> errorsMessage)
				: base(null)
		{
			this.ErrorsMessage = errorsMessage;
		}

		public BulkEditModelException(IEnumerable<ModelError> errorsMessage, Exception inner)
				: base(null, inner)
		{
			this.ErrorsMessage = errorsMessage;
		}

		public IEnumerable<ModelError> ErrorsMessage { get; }
	}
}
