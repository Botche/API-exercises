namespace LibraryAPI.Infrastructure.Filters
{
	using System.Linq;

	using Microsoft.OpenApi.Models;

	using Swashbuckle.AspNetCore.SwaggerGen;

	public class CustomDocumentFilter : IDocumentFilter
	{
		public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
		{
			swaggerDoc.Paths.Clear();

			//make operations alphabetic
			var paths = swaggerDoc.Paths.OrderBy(e => e.Key).ToList();
			foreach (var path in paths)
			{
				swaggerDoc.Paths.Add(path.Key, path.Value);
			}
		}
	}
}
