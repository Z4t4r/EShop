using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace EShopSample.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class EShopSampleMigrationsDbContextFactory : IDesignTimeDbContextFactory<EShopSampleMigrationsDbContext>
    {
        public EShopSampleMigrationsDbContext CreateDbContext(string[] args)
        {
            EShopSampleEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            //var builder = new DbContextOptionsBuilder<EShopSampleMigrationsDbContext>()
            //    .UseSqlServer(configuration.GetConnectionString("Default"));
            var tt = configuration.GetConnectionString("Default");
            //var serverVersion = new ServerVersion(new Version(8, 0, 19), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql);
            var builder = new DbContextOptionsBuilder<EShopSampleMigrationsDbContext>()
                .UseMySql(tt,new MySqlServerVersion(new Version(8,0,21)));
            return new EShopSampleMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
