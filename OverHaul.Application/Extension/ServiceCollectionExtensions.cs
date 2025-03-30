using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.Public;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using Overhaul.Application.Swagger;

using Overhaul.Application.Authentication;
using Overhaul.Application.Utilitty;

namespace Overhaul.Application.Extensions;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerGenerator(this IServiceCollection services)
    {
        return services.AddSwaggerGen(
            c =>
            {
                var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                //var filePath = Path.Combine(
                //    path1: AppContext.BaseDirectory,
                //    path2: assemblyName + ".xml"
                //);
                //c.IncludeXmlComments(filePath);
                c.AddSecurityDefinition(
                    name: "Bearer",
                    securityScheme: new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Description =
                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer"
                    }
                );
                c.SchemaFilter<EnumSchemaFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            }
        );
    }
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenSetting = configuration.GetSection(nameof(SiteSetting)).Get<SiteSetting>().TokenSetting;
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var secretkey = Encoding.UTF8.GetBytes(tokenSetting.SecretKey);
            var encryptionkey = Encoding.UTF8.GetBytes(tokenSetting.EncryptKey);
            var validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretkey),
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidAudience = tokenSetting.Audience,
                ValidateIssuer = true,
                ValidIssuer = tokenSetting.Issuer,
                TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey),
                NameClaimType = ClaimTypes.NameIdentifier
            };
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = validationParameters;
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception != null)
                    {
                        var errorFactory = context.HttpContext.RequestServices.GetRequiredService<IErrorFactory>();
                        context.Fail(errorFactory.AccessDenied());
                    }
                    return Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    var errorFactory = context.HttpContext.RequestServices.GetRequiredService<IErrorFactory>();
                    var tokenMangerContext = context.HttpContext.RequestServices.GetRequiredService<ITokenManagerService>();
                    var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                    var userClaim = claimsIdentity.FindFirst("UserId");
                    var securityStamp = claimsIdentity.FindFirst("security-stamp");
                    var checkLoggedUser = await tokenMangerContext.IsActiveToken();
                    var IsValidUser = true;// await tokenMangerContext.CheckSecurityStamp(userClaim.Value, securityStamp.Value);
                    if (checkLoggedUser == false)
                    {
                        context.Fail(errorFactory.InvalidToken());
                    }
                    if (IsValidUser == false)
                    {
                        context.Fail(errorFactory.ThisUserIsAltered());
                    }
                },
                OnChallenge = context =>
                {
                    var errorFactory = context.HttpContext.RequestServices.GetRequiredService<IErrorFactory>();
                    if (context.AuthenticateFailure != null)
                    {
                        return Task.FromException(errorFactory.AccessDenied());
                    }
                    else
                    {
                        return Task.FromException(errorFactory.AccessDenied());
                    }
                }
            };
        });
    }
    public static void AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        var siteSetting = configuration.GetSection(nameof(SiteSetting)).Get<SiteSetting>();
        var withOrigins = siteSetting.WithOrigins.Split(",");
        services.AddCors(options =>
        {
            options.AddPolicy(siteSetting.AllowSpecificOrigins,
           builder =>
           {
               builder.WithOrigins(withOrigins)
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
           });
        });
    }
    //        public static void AddJwtAuthentication(
    //    this IServiceCollection services,
    //    IConfiguration configuration
    //)
    //        {
    //            var tokenSetting = configuration
    //                .GetSection("TokenSettings")
    //                .Get<IDictionary<string, TokenSetting>>()["Token"];
    //            services
    //                .AddAuthentication(
    //                    options =>
    //                    {
    //                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    //                    }
    //                )
    //                .AddJwtBearer(
    //                    options =>
    //                    {
    //                        var secretKey = Encoding.UTF8.GetBytes(tokenSetting.SecretKey);
    //                        var encryptionKey = Encoding.UTF8.GetBytes(tokenSetting.EncryptKey);
    //                        var validationParameters = new TokenValidationParameters
    //                        {
    //                            ClockSkew = TimeSpan.Zero,
    //                            RequireSignedTokens = true,
    //                            ValidateIssuerSigningKey = true,
    //                            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
    //                            RequireExpirationTime = true,
    //                            ValidateLifetime = true,
    //                            ValidateAudience = true,
    //                            ValidAudience = tokenSetting.Audience,
    //                            ValidateIssuer = true,
    //                            ValidIssuer = tokenSetting.Issuer,
    //                            TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey),
    //                            NameClaimType = ClaimTypes.NameIdentifier
    //                        };
    //                        options.RequireHttpsMetadata = false;
    //                        options.SaveToken = true;
    //                        options.TokenValidationParameters = validationParameters;
    //                        options.Events = new JwtBearerEvents
    //                        {
    //                            OnAuthenticationFailed = context =>
    //                            {
    //                                if (context.Exception != null)
    //                                {
    //                                    var errorFactory =
    //                                        context.HttpContext.RequestServices.GetRequiredService<IErrorFactory>();
    //                                    context.Fail(errorFactory.InvalidToken());
    //                                }

    //                                return Task.CompletedTask;
    //                            },
    //                            OnTokenValidated = async context =>
    //                            {
    //                                var errorFactory =
    //                                    context.HttpContext.RequestServices.GetRequiredService<IErrorFactory>();
    //                                var tokenMangerContext =
    //                                    context.HttpContext.RequestServices.GetRequiredService<ITokenManagerService>();
    //                                var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
    //                                var userClaim = claimsIdentity.FindFirst("user-id");
    //                                var securityStamp = claimsIdentity.FindFirst("security-stamp");
    //                                var checkLoggedUser = await tokenMangerContext.IsActiveToken();
    //                                if (!checkLoggedUser)
    //                                    context.Fail(errorFactory.InvalidToken());
    //                                var IsValidUser = await tokenMangerContext.CheckSecurityStamp(
    //                                    userId: userClaim.Value,
    //                                    hashValue: securityStamp.Value
    //                                );
    //                                if (!IsValidUser)
    //                                    context.Fail(errorFactory.ThisUserIsAltered());
    //                            },
    //                            OnChallenge = context =>
    //                            {
    //                                var errorFactory =
    //                                    context.HttpContext.RequestServices.GetRequiredService<IErrorFactory>();
    //                                if (context.AuthenticateFailure != null)
    //                                    return Task.FromException(errorFactory.InvalidToken());
    //                                else
    //                                    return Task.FromException(errorFactory.AccessDenied());
    //                            }
    //                        };
    //                    }
    //                );
    //        }
}
