using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Overhaul.Infrastructuer.AutoFac;
using Overhaul.Infrastructuer.Data;
using System.Configuration;
using Serilog;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataTables.AspNet.AspNetCore;
using Overhaul.Application.Contracts;
using Overhaul.Infrastructure.Tools;
using Overhaul.Domain.Entities;
using Overhaul.Domain.Common;
using Overhaul.AdminPanel;
using Overhaul.Application.Models;
using Overhaul.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Overhaul.Infrastructure.Extentions;
using Stimulsoft.Base;


var builder = WebApplication.CreateBuilder(args);

StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHkO46nMQvol4ASeg91in+mGJLnn2KMIpg3eSXQSgaFOm15+0l" +
"hekKip+wRGMwXsKpHAkTvorOFqnpF9rchcYoxHXtjNDLiDHZGTIWq6D/2q4k/eiJm9fV6FdaJIUbWGS3whFWRLPHWC" +
"BsWnalqTdZlP9knjaWclfjmUKf2Ksc5btMD6pmR7ZHQfHXfdgYK7tLR1rqtxYxBzOPq3LIBvd3spkQhKb07LTZQoyQ" +
"3vmRSMALmJSS6ovIS59XPS+oSm8wgvuRFqE1im111GROa7Ww3tNJTA45lkbXX+SocdwXvEZyaaq61Uc1dBg+4uFRxv" +
"yRWvX5WDmJz1X0VLIbHpcIjdEDJUvVAN7Z+FW5xKsV5ySPs8aegsY9ndn4DmoZ1kWvzUaz+E1mxMbOd3tyaNnmVhPZ" +
"eIBILmKJGN0BwnnI5fu6JHMM/9QR2tMO1Z4pIwae4P92gKBrt0MqhvnU1Q6kIaPPuG2XBIvAWykVeH2a9EP6064e11" +
"PFCBX4gEpJ3XFD0peE5+ddZh+h495qUc1H2B";

var postgres = builder.Configuration.GetConnectionString("OverHaul");


builder.Services.AddDbContext<ApplicationDbContext>(
    options =>
    {
        options.UseSqlServer(
            connectionString: postgres
        );
    }
);

//builder.Services.AddDbContext<ApplicationDbContext>(
//    options =>
//    {
//        options.UseNpgsql(
//            connectionString: postgres
//        );
//        options.EnableSensitiveDataLogging();
//    }
//);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.RegisterDataTables();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IDataExporterFactory, DataExporterFactory>();

//builder.Services.AddAutoMapper(typeof(MappingSaleType));

builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                      .AddCookie(options =>
                      {
                          options.AccessDeniedPath = "/Account/AccessDenied";
                          options.Cookie.Name = "WelFareAdminCookie";
                          options.Cookie.HttpOnly = true;
                          options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                          options.LoginPath = "/Auth/Login";
                          options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                          options.SlidingExpiration = true;
                      });

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.AddAutofacDependencyServices();
});
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
app.UseSession();
//app.ApplyMigrations();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.UseStaticFiles();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
