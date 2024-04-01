using InventoryManagement.Models.Options;

namespace InventoryManagement.ModuleRegistrations
{
    public static class OptionCollection
    {
        public static IServiceCollection AddOptionCollection(this IServiceCollection services,
           IConfiguration configuration)
        {
            return services
                .Configure<CloudinaryOption>(option => configuration.GetSection(CloudinaryOption.Position).Bind(option))
            ;
        }
    }
}
