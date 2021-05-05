using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PL_ASP_5.Models
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GIBDDContext>

    {
        public GIBDDContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new
 ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<GIBDDContext>();

            var connectionString = configuration.GetConnectionString("DevConnection");

            builder.UseSqlServer(connectionString);
            return new GIBDDContext(builder.Options);
        }
    }
}
