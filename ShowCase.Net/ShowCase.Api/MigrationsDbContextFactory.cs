using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShowCase.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShowCase.Api
{
    public class MigrationsDbContextFactory
    {
        public DataDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DataDbContext>();
            var connectionString = configuration.GetConnectionString("DataConnection");
            builder.UseSqlite(connectionString);

            return new DataDbContext(builder.Options);
        }
    }
}
