namespace Test.Database
{
	using Microsoft.EntityFrameworkCore;

	public class ApplicationDbContext : DbContext
	{
		public DbSet<WeatherForecast> WeatherForecasts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer("Server=.;Database=PeopleAPITest;Integrated Security = true;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
