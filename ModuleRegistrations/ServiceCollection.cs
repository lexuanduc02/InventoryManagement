using InventoryManagement.Services;
using InventoryManagement.Services.Contractors;

namespace InventoryManagement.ModuleRegistrations
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServiceCollection(this IServiceCollection services)
        {
            services
                .AddScoped<IWarehouseService, WarehouseService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IPartnerService, PartnerService>()
            ;

            return services;
        }
    }
}
