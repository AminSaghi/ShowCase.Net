using Mapster;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.ApiModels.Feature;
using ShowCase.Data.Models.ApiModels.Page;
using ShowCase.Data.Models.ApiModels.Project;
using ShowCase.Data.Models.ApiModels.Settings;
using ShowCase.Data.Models.ApiModels.User;
using ShowCase.Data.Models.Entities;
using ShowCase.Service.DataManagers;
using ShowCase.Util.StaticClasses;
using System;
using System.Text;

namespace ShowCase.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<DataDbContext>(options =>
            //    options.UseSqlite(Configuration.GetConnectionString("DataConnection")), ServiceLifetime.Transient);            

            services.AddDbContext<DataDbContext>(ServiceLifetime.Scoped);            

            services.AddScoped<PageManager>();
            services.AddScoped<ProjectManager>();
            services.AddScoped<FeatureManager>();
            services.AddScoped<SettingsManager>();

            services.AddCors(options => options.AddPolicy("CORS",
                builder =>
                {
                    builder                    
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                }));

            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = false;

                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 7;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DataDbContext>();

            services.AddScoped<UserManager<IdentityUser>>();
            services.AddScoped<SignInManager<IdentityUser>>();            

            // Add Authentication with JWT Tokens
            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultAuthenticateScheme =
                JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    //// standard configuration
                    ValidIssuer = SecuritySettings.JwtIssuer,
                    ValidAudience = SecuritySettings.JwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecuritySettings.JwtSecret)),
                    ClockSkew = TimeSpan.Zero,

                    // security switches
                    RequireExpirationTime = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false
                };
            });

            // Angular's default header name for sending the XSRF token.
            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddMvc(options =>
            {
                //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IAntiforgery antiforgery)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CORS");            
            app.UseAuthentication();
            app.UseMvc();

            MapsterConfig();
        }

        private static void MapsterConfig()
        {
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
            TypeAdapterConfig.GlobalSettings.Default.IgnoreNonMapped(true);

            #region Page

            TypeAdapterConfig<Page, ListPagesApiModel>
                .ForType()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.orderIndex, src => src.OrderIndex)
                .Map(dest => dest.title, src => src.Title)
                .Map(dest => dest.slug, src => src.Slug)
                .Map(dest => dest.updateDateTime,
                    src => src.UpdateDateTime.LocalDateTime.ToString("yyyy-MM-dd HH:mm"))
                .Map(dest => dest.published, src => src.Published)
                .Map(dest => dest.children, src => src.Children);

            TypeAdapterConfig<Page, PageApiModel>
                .ForType()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.orderIndex, src => src.OrderIndex)
                .Map(dest => dest.title, src => src.Title)
                .Map(dest => dest.slug, src => src.Slug)
                .Map(dest => dest.content, src => src.Content)
                .Map(dest => dest.createDateTime,
                    src => src.CreateDateTime.LocalDateTime.ToString("yyyy-MM-dd HH:mm"))
                .Map(dest => dest.updateDateTime,
                    src => src.UpdateDateTime.LocalDateTime.ToString("yyyy-MM-dd HH:mm"))
                .Map(dest => dest.published, src => src.Published)
                .Map(dest => dest.parent, src => src.Parent)
                .Map(dest => dest.children, src => src.Children);

            #endregion

            #region Project

            TypeAdapterConfig<Project, ListProjectsApiModel>
                .ForType()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.orderIndex, src => src.OrderIndex)
                .Map(dest => dest.title, src => src.Title)
                .Map(dest => dest.slug, src => src.Slug)
                .Map(dest => dest.description, src => src.Description)
                .Map(dest => dest.published, src => src.Published);

            TypeAdapterConfig<Project, ProjectApiModel>
                .ForType()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.orderIndex, src => src.OrderIndex)
                .Map(dest => dest.title, src => src.Title)
                .Map(dest => dest.slug, src => src.Slug)
                .Map(dest => dest.description, src => src.Description)
                .Map(dest => dest.imageUrl, src => src.ImageUrl)
                .Map(dest => dest.published, src => src.Published)
                .Map(dest => dest.features, src => src.Features);

            #endregion

            #region Feature

            TypeAdapterConfig<Feature, ListFeaturesApiModel>
                .ForType()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.orderIndex, src => src.OrderIndex)
                .Map(dest => dest.title, src => src.Title)
                .Map(dest => dest.slug, src => src.Slug)
                .Map(dest => dest.updateDateTime,
                    src => src.UpdateDateTime.LocalDateTime.ToString("yyyy-MM-dd HH:mm"))
                .Map(dest => dest.published, src => src.Published)
                .Map(dest => dest.project, src => src.Project)
                .Map(dest => dest.children, src => src.Children);

            TypeAdapterConfig<Feature, FeatureApiModel>
                .ForType()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.orderIndex, src => src.OrderIndex)
                .Map(dest => dest.title, src => src.Title)
                .Map(dest => dest.slug, src => src.Slug)
                .Map(dest => dest.content, src => src.Content)
                .Map(dest => dest.createDateTime,
                    src => src.CreateDateTime.LocalDateTime.ToString("yyyy-MM-dd HH:mm"))
                .Map(dest => dest.updateDateTime,
                    src => src.UpdateDateTime.LocalDateTime.ToString("yyyy-MM-dd HH:mm"))
                .Map(dest => dest.published, src => src.Published)
                .Map(dest => dest.project, src => src.Project)
                .Map(dest => dest.parent, src => src.Parent)
                .Map(dest => dest.children, src => src.Children);

            #endregion

            #region User

            TypeAdapterConfig<IdentityUser, ListUsersApiModel>
                .ForType()
                .Map(dest => dest.id, src => src.Id)
                .Map(dest => dest.userName, src => src.UserName)
                .Map(dest => dest.email, src => src.Email)
                .Map(dest => dest.status, src => src.LockoutEnabled && src.LockoutEnd.HasValue && src.LockoutEnd.Value > DateTimeOffset.Now ?
                    "Locked out" : "Active");       

            #endregion

            #region Settings

            TypeAdapterConfig<Settings, SettingsApiModel>
                .ForType()
                .Map(dest => dest.logoUrl, src => src.LogoUrl)
                .Map(dest => dest.footerContent, src => src.FooterContent);            

            #endregion
        }
    }
}
