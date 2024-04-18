using InventoryManagement.Domains.EF;
using InventoryManagement.Repositories.Contractors;
using InventoryManagement.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.ModuleRegistrations
{
    public static class RepositoryCollection
    {
        public static IServiceCollection AddRepositoryCollection(this IServiceCollection services, string connectionString)
        {
            services
                .AddDbContext<DataContext>(
                        option => option.UseSqlServer(connectionString));

            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IReportRepository, ReportRepository>()
                .AddScoped<IPurchaseInvoiceRepository, PurchaseInvoiceRepository>()
            ;

            return services;
        }
    }
}
