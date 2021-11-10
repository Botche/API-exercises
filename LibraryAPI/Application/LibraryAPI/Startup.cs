namespace LibraryAPI
{
	using System;
	using System.IO;
	using System.Reflection;

	using LibraryAPI.Database;
	using LibraryAPI.Infrastructure.Extensions;
	using LibraryAPI.Infrastructure.Middlewares;
	using LibraryAPI.Services.Database;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.OpenApi.Models;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			// Swagger
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryAPI", Version = "v1" });

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});

			// Database
			services.AddDbContext<LibraryAPIDbContext>();

			// AutoMapper
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			// Database services
			this.AddDatabaseServices(services);

			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryAPI v1"));
			}
			
			app.ConfigureCustomExceptionMiddleware();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private void AddDatabaseServices(IServiceCollection services)
		{
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IGenreService, GenreService>();
			services.AddScoped<IBookGenreMappingService, BookGenreMappingService>();
		}
	}
}
