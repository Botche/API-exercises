namespace LibraryAPI.Infrastructure.Extensions
{
	using LibraryAPI.Infrastructure.Middlewares;

	using Microsoft.AspNetCore.Builder;

	public static class CustomMiddlewareExtensions
	{
		public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
