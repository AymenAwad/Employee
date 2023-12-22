using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Domain.Entities.Identity;
using Localization.Resources;   
using Persistence.Contexts;
using Shared.DTOs;
using Shared.Interfaces.Services;
using Shared.Interfaces.Settings;
using Shared.Services;
using Shared.Settings;
using Shared.Wrappers;
using Common.Services;
using Application.Interface.Services;
using Infrastructure.Implementation;
using Application.Interfaces;
using Application.Interface.Interfaces;
using Infrastructure.Implementation.Services;
using Infrastructure.Repositories.Services;

namespace Infrastructure.Extension
{
    public static class ConfigureServiceContainer
    {
        public static void AddDbContext(this IServiceCollection serviceCollection,
             IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") 
                , b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        }

        public static void AddAutoMapper(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                // mc.AddProfile(new CustomerProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }

        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        }
       
        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {
            
            serviceCollection.AddTransient<IDateTimeService, DateTimeService>();
            serviceCollection.AddTransient<IAccountService, AccountService>();
            serviceCollection.AddTransient<IEmailService, EmailService>();
            //serviceCollection.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();

            serviceCollection.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            serviceCollection.AddTransient<IEmployee, EmployeeServices>();

            serviceCollection.AddTransient<IUnitOfWork, UnitOfWork>();
           
        }

        public static void AddSwaggerOpenAPI(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(setupAction =>
            {

                setupAction.SwaggerDoc(
                    "OpenAPISpecification",
                    new OpenApiInfo()
                    {
                        Title = "Resyaurant WebAPI",
                        Version = "1",
                        Description = "Through this API you can access ...",
                        Contact = new OpenApiContact()
                        {
                            Email = "mmali@master-works.sa",
                            Name = "Master Works",
                            Url = new Uri("https://localhost:7213/swagger")
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "All Copy by",
                            Url = new Uri("https://master-works.net/")
                        }
                    });

                setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Description = $"Input your Bearer token in this format - Bearer token to access this API",
                });
                setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        }, new List<string>()
                    },
                });
            });

        }

        public static void AddMailSetting(this IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            //  serviceCollection.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            MailSettings mailSettings = new MailSettings();
            configuration.Bind("MailSettings", mailSettings);
            serviceCollection.AddSingleton(mailSettings);
        }

        public static void AddController(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers().AddNewtonsoftJson();
        }

        public static void AddVersion(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }


        public static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            //{
            //    services.AddDbContext<IdentityContext>(options =>
            //        options.UseInMemoryDatabase("IdentityDb"));
            //}
            //else
            //{
            //    services.AddDbContext<IdentityContext>(options =>
            //    options.UseSqlServer(
            //        configuration.GetConnectionString("IdentityConnection"),
            //        b => b.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
            //}

            VirtualPathSettingDto virtualPathSetting = new VirtualPathSettingDto();
            configuration.Bind("VirtualPathSetting", virtualPathSetting);
            services.AddSingleton(virtualPathSetting);
            services.AddTransient<IVirtualPathService, VirtualPathService>();


            services.AddIdentity<ApplicationUser, Role>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            #region Services
            // services.AddTransient<IAccountService, AccountService>();
            #endregion
       
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                       // ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JWTSettings:Issuer"],
                        ValidAudience = configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });
        }



        public static void AddMultiLingualSupport(this IServiceCollection services)
        {
            #region Registering ResourcesPath

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            #endregion Registering ResourcesPath

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResource));
                });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var cultures = new List<CultureInfo> {
                     new CultureInfo("en"),
                     new CultureInfo("ar")                };
                options.DefaultRequestCulture = new RequestCulture("ar");
                options.SupportedCultures = cultures;
                options.SupportedUICultures = cultures;

                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });
        }

        public static void UseMultiLingualFeature(this IApplicationBuilder app)
        {
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", x => x.AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyOrigin());

            });
        }
    }
}
