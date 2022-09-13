using Microsoft.EntityFrameworkCore;
using SectorsBackend.Data;

namespace SectorsBackend.UnitTests.RepositoriesTests
{
	public class TestDbContextFactory : IDbContextFactory<AppDbContext>
	{
		private DbContextOptions<AppDbContext> _options;
		public TestDbContextFactory(string dbName = "InMemoryDB")
        {
			_options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
		}

		public AppDbContext CreateDbContext()
		{
			return new AppDbContext(_options);
		}
		
	}
}
