using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InventoryManagement.Domains.EF
{
    public class ContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        //private readonly IConfiguration _configuration;

        //public ContextFactory(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public DataContext CreateDbContext(string[] args)
        {
            //var connectionString = _configuration.GetConnectionString("LMSDatabase");

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            optionsBuilder.UseSqlServer("Server=.;Database=InventoryManagement;Trusted_Connection=True;TrustServerCertificate=True;");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
