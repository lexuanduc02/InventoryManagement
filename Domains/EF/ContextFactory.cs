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

            optionsBuilder.UseSqlServer("Data Source=mssql.cpqyemq62wqv.ap-southeast-2.rds.amazonaws.com,1433;Initial Catalog=InventoryManagement;Persist Security Info=True;User ID=admin;Password=13.4+72KoQ8Y;Trust Server Certificate=True");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
