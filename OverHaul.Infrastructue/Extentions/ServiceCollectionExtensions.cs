using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Overhaul.Infrastructuer.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Infrastructuer.Extentions;

public static class ServiceCollectionExtensions
{
    public static void AddContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<ApplicationDbContext>(
             options => options.UseSqlServer(configuration.GetConnectionString("OverHaul"))
         );
    }

    //public static void AddCors(this IServiceCollection services, IConfiguration configuration)
    //{
    //    var withOrigins = configuration.GetSection("WithOrigins").Get<string>().Split(",");
    //    var allowSpecificOrigins = configuration
    //        .GetSection("AllowSpecificOrigins")
    //        .Get<string>();
    //    var allowAnyOrigin = configuration.GetSection("AllowAnyOrigins").Get<bool>();

    //    services.AddCors(
    //        options =>
    //        {
    //            options.AddPolicy(
    //                name: allowSpecificOrigins,
    //                configurePolicy: builder =>
    //                {
    //                    builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials();

    //                    if (allowAnyOrigin)
    //                        // allow any origin
    //                        builder.SetIsOriginAllowed(_ => true);
    //                    else
    //                        builder.WithOrigins(withOrigins);
    //                }
    //            );
    //        }
    //    );
    //}
}
