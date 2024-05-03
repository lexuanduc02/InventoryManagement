using InventoryManagement.Domains.EF;
using InventoryManagement.Middlewares;
using InventoryManagement.ModuleRegistrations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false, true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                        .Build();

var connectionString = builder.Configuration.GetConnectionString("database");
if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("=======Has not define connection string yet!!!=======");
    return;
}

// Add services to the container.
var services = builder.Services;

services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation()
; 

services
    .AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });

services
    .AddServiceCollection()
    .AddAutoMapper(typeof(Program))
    .AddOptionCollection(configuration)
    .AddRepositoryCollection(connectionString)
;

services
    .AddRouting(options =>
    {
        options.LowercaseUrls = true;
    });

services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Oauth/Forbidden/";
        options.LoginPath = "/Oauth/Login/";
        options.LogoutPath = "/Oauth/Logout/";
    });

services.AddAuthorizationCollection();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseMiddleware<NotFoundMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}").RequireAuthorization();

app.Run();
