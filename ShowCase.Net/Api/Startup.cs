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
        }
    }
}
