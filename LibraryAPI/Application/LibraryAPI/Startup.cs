namespace LibraryAPI
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;

	using LibraryAPI.Common;
	using LibraryAPI.Common.Constants;
	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Users;
	using LibraryAPI.DTOs.User;
	using LibraryAPI.Infrastructure.Extensions;
	using LibraryAPI.Infrastructure.Middlewares;
	using LibraryAPI.Services.Database;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.AspNetCore.Builder;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.EntityFrameworkCore;
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

			// Configure strongly typed settings object
			services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

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

				MigrateDatabase(app).GetAwaiter().GetResult();
			}

			app.ConfigureCustomExceptionMiddleware();

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseMiddleware<JwtMiddleware>();

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
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IRoleService, RoleService>();
			services.AddScoped<IUserRoleMappingService, UserRoleMappingService>();
		}

		private async static Task MigrateDatabase(IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var services = scope.ServiceProvider;
				var dbContext = services.GetRequiredService<LibraryAPIDbContext>();
				using (dbContext)
				{
					var userService = services.GetRequiredService<IUserService>();
					var userRoleMappingService = services.GetRequiredService<IUserRoleMappingService>();

					dbContext.Database.Migrate();

					await SeedRolesAsync(dbContext);
					await SeedUsersAsync(dbContext, userService);
					await SeedUserRoleMappingsAsync (dbContext, userRoleMappingService);
				}
			}
		}

		private async static Task SeedUserRoleMappingsAsync(LibraryAPIDbContext dbContext, IUserRoleMappingService userRoleMappingService)
		{
			if (dbContext.UsersRolesMapping.Any() == false)
			{
				Guid userId = await dbContext.Users
					.Where(u => u.Email == GlobalConstants.USER_EMAIL)
					.Select(u => u.Id)
					.SingleOrDefaultAsync();
				Guid adminId = await dbContext.Users
					.Where(u => u.Email == GlobalConstants.ADMIN_EMAIL)
					.Select(u => u.Id)
					.SingleOrDefaultAsync();

				Guid userRoleId = await dbContext.Roles
					.Where(u => u.Name == GlobalConstants.USER_ROLE_NAME)
					.Select(u => u.Id)
					.SingleOrDefaultAsync();
				Guid adminRoleId = await dbContext.Roles
					.Where(u => u.Name == GlobalConstants.ADMIN_ROLE_NAME)
					.Select(u => u.Id)
					.SingleOrDefaultAsync();

				await userRoleMappingService.AddRoleToUserAsync(userRoleId, userId);
				await userRoleMappingService.AddRoleToUserAsync(adminRoleId, adminId);
			}
		}

		private async static Task SeedUsersAsync(LibraryAPIDbContext dbContext, IUserService userService)
		{
			if (dbContext.Users.Any() == false)
			{
				string userSalt = userService.GeneratePasswordSalt();
				string userPasswordHash = userService.HashPassword(GlobalConstants.USER_PASSWORD, userSalt);
				User userModel = new User()
				{
					Email = GlobalConstants.USER_EMAIL,
					FirstName = GlobalConstants.USER_FIRST_NAME,
					LastName = GlobalConstants.USER_LAST_NAME,
					PasswordHash = userPasswordHash,
					Salt = userSalt,
				};

				string adminSalt = userService.GeneratePasswordSalt();
				string adminPasswordHash = userService.HashPassword(GlobalConstants.ADMIN_PASSWORD, adminSalt);
				User adminModel = new User()
				{
					Email = GlobalConstants.ADMIN_EMAIL,
					FirstName = GlobalConstants.ADMIN_FIRST_NAME,
					LastName = GlobalConstants.ADMIN_LAST_NAME,
					PasswordHash = adminPasswordHash,
					Salt = adminSalt,
				};

				await dbContext.AddAsync(userModel);
				await dbContext.AddAsync(adminModel);
				await dbContext.SaveChangesAsync();
			}
		}

		private async static Task SeedRolesAsync(LibraryAPIDbContext dbContext)
		{
			if (dbContext.Roles.Any() == false)
			{
				Role userRole = new Role()
				{
					Name = GlobalConstants.USER_ROLE_NAME,
				};

				Role adminRole = new Role()
				{
					Name = GlobalConstants.ADMIN_ROLE_NAME,
				};

				await dbContext.AddAsync(userRole);
				await dbContext.AddAsync(adminRole);
				await dbContext.SaveChangesAsync();
			}
		}
	}
}
