using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InventoryManagement.Domains.EF
{
    public class ContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                        .Build();

            var connectionString = configuration.GetConnectionString("database");

            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            //optionsBuilder.UseSqlServer("Data Source=mssql.cpqyemq62wqv.ap-southeast-2.rds.amazonaws.com,1433;Initial Catalog=InventoryManagement;Persist Security Info=True;User ID=admin;Password=13.4+72KoQ8Y;Trust Server Certificate=True");
            optionsBuilder.UseSqlServer(connectionString);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
