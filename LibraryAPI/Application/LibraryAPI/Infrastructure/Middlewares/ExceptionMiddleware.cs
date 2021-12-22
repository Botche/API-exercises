namespace LibraryAPI.Infrastructure.Middlewares
{
	using System;
	using System.Net;
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Http;

	using LibraryAPI.Common;
	using LibraryAPI.Common.Exceptions;
	using System.Linq;

	public class ExceptionMiddleware
	{
		private readonly RequestDelegate next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			this.next = next;
		}
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			ErrorDetails errorDetails = new ErrorDetails
			{
				Message = exception.Message,
			};

			switch (exception)
			{
				case BulkEditModelException:
					BulkEditModelException bulkEditModelException = exception as BulkEditModelException;
					errorDetails.Message = bulkEditModelException.ErrorsMessage
						.Select(e => e.ErrorMessage);

					break;
				case EntityDoesNotExistException:
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;
					break;
				case UnauthorizedAccessCustomException:
					context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					break;
			}

			errorDetails.StatusCode = context.Response.StatusCode;

      string result = errorDetails.ToString();
			await context.Response.WriteAsync(result);
		}
	}
}
