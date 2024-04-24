using InventoryManagement.Services;
using InventoryManagement.Services.Contractors;
using Slugify;

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
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IImageService, ImageService>()
                .AddScoped<ISlugHelper, SlugHelper>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<ISaleInvoiceService, SaleInvoiceService>()
                .AddScoped<IMerchandiseSaleInvoiceService, MerchandiseSaleInvoiceService>()
                .AddScoped<IPurchaseInvoiceService, PurchaseInvoiceService>()
                .AddScoped<IMerchandisePurchaseInvoiceService, MerchandisePurchaseInvoiceService>()
                .AddScoped<IReportService, ReportService>()
                .AddScoped<IOauthService, OauthService>()
                .AddScoped<IPartialViewService, PartialViewService>()
                .AddScoped<IPdfService, PdfService>()
            ;

            return services;
        }
    }
}
