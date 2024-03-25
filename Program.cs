using InventoryManagement.Domains.EF;
using Microsoft.EntityFrameworkCore;

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

services.AddControllersWithViews();

services
    .AddDbContext<DataContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });

services
    .AddRouting(options =>
    {
        options.LowercaseUrls = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
