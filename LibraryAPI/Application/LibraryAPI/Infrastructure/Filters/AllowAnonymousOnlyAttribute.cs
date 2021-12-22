namespace LibraryAPI.Infrastructure.Filters
{
  using System;
	using System.Linq;

	using LibraryAPI.Common.Constants;

	using Microsoft.AspNetCore.Mvc.Filters;

	public class AllowAnonymousOnlyAttribute : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var token = context
          .HttpContext
          .Request
          .Headers["Authorization"]
          .FirstOrDefault()
          ?.Split(" ")
          .Last();

      if (token != null)
      {
        throw new InvalidOperationException(ExceptionMessages.ANONYMOUS_PAGE);
      }

      base.OnActionExecuting(context);
    }
  }
}
