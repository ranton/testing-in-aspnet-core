using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace SampleWebApplication.Tests
{
	public class InMemoryEfCoreTests
    {
		[Fact]
		public void GetRecipeDetails_CanLoadFromContext()
		{
			var connection = new SqliteConnection("DataSource=:memory:");
			connection.Open();

			var options = new DbContextOptionsBuilder<AppDbContext>()
				.UseSqlite(connection)
				.Options;

			using (var context = new AppDbContext(options))
			{
				context.Database.EnsureCreated();
				context.Weathers.AddRange(
				new Weather { Id = 1, Name = "Snow" },
				new Weather { Id = 2, Name = "Summer" },
				new Weather { Id = 3, Name = "Spring" });
				context.SaveChanges();
			}

			using (var context = new AppDbContext(options))
			{
				var service = new WeatherService(context);
				var weather = service.GetRecipe(id: 2);
				Assert.NotNull(weather);
				Assert.Equal(2, weather.Id);
				Assert.Equal("Summer", weather.Name);
			}
		}

	}
}
