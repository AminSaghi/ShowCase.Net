using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShowCase.Data.DbContexts;
using ShowCase.Data.Models.ApiModels.Page;
using ShowCase.Data.Models.Entities;

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
            services.AddDbContext<DataDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DataConnection")), ServiceLifetime.Transient);

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

            //TODO: mapping for PageApiModel.

            #endregion
        }
    }
}
