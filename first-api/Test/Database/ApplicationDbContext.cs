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

			modelBuilder.Entity<WeatherForecast>()
				.Property(wf => wf.TemperatureF)
				// here is the computed query definition
				.HasComputedColumnSql("CAST((32 + ([TemperatureC] / 0.5556)) as INTEGER)");
		}
	}
}
