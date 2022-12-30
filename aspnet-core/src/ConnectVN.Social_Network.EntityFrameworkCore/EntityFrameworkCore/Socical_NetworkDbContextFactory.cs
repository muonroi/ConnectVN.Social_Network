using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ConnectVN.Social_Network.Common.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class Social_NetworkDbContextFactory : IDesignTimeDbContextFactory<Social_NetworkDbContext>
{
    public Social_NetworkDbContext CreateDbContext(string[] args)
    {
        Social_NetworkEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<Social_NetworkDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new Social_NetworkDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ConnectVN.Social_Network.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
