using EmployeeHR.Components;
using EmployeeHR.Data;
using EmployeeHR.Middelwares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//IConfiguration configuration = new ConfigurationBuilder()
//              .SetBasePath(Directory.GetCurrentDirectory())
//              .AddJsonFile("appsettings.json")
//              .Build();
//var connectionString = configuration.GetConnectionString("HRConnectionString");

builder.Services.AddDbContext<HRDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("HRConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<HRDbContext>();

builder.Services.AddScoped<EmployeesCount>();


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

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.UseCustomMiddleware(); 
app.Run();
