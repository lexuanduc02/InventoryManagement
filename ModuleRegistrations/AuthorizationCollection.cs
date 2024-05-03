using InventoryManagement.Commons.Enums;

namespace InventoryManagement.ModuleRegistrations
{
    public static class AuthorizationCollection
    {
        public static IServiceCollection AddAuthorizationCollection(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", policy =>
                {
                    policy.RequireRole(AuthorizationEnum.Admin.ToString());
                });

                options.AddPolicy("sale", policy =>
                {
                    policy.RequireRole(AuthorizationEnum.Sale.ToString(), 
                        AuthorizationEnum.Admin.ToString());
                });

                options.AddPolicy("warehouse", policy =>
                {
                    policy.RequireRole(AuthorizationEnum.Warehouse.ToString(), 
                        AuthorizationEnum.Admin.ToString());
                });

                options.AddPolicy("report", policy =>
                {
                    policy.RequireRole(AuthorizationEnum.Secretary.ToString(), 
                        AuthorizationEnum.Admin.ToString());
                });

                options.AddPolicy("purchase", policy =>
                {
                    policy.RequireRole(AuthorizationEnum.Purchase.ToString(),
                        AuthorizationEnum.Admin.ToString());
                });

                options.AddPolicy("productAccess", policy =>
                {
                    policy.RequireRole(AuthorizationEnum.Warehouse.ToString(),
                        AuthorizationEnum.Sale.ToString(),
                        AuthorizationEnum.Admin.ToString(),
                        AuthorizationEnum.Purchase.ToString()
                    );
                });

                options.AddPolicy("warehouseReport", policy =>
                {
                    policy.RequireRole(AuthorizationEnum.Warehouse.ToString(),
                        AuthorizationEnum.Admin.ToString(),
                        AuthorizationEnum.Secretary.ToString()
                    );
                });

                options.AddPolicy("updateWarehouse", policy =>
                {
                    policy.RequireRole(AuthorizationEnum.Warehouse.ToString(),
                        AuthorizationEnum.Admin.ToString()
                    );
                });
            });

            return services;
        }
    }
}
