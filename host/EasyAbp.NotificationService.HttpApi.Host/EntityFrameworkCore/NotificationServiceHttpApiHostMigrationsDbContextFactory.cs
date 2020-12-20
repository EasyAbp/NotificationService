using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasyAbp.NotificationService.EntityFrameworkCore
{
    public class NotificationServiceHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<NotificationServiceHttpApiHostMigrationsDbContext>
    {
        public NotificationServiceHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<NotificationServiceHttpApiHostMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("NotificationService"));

            return new NotificationServiceHttpApiHostMigrationsDbContext(builder.Options);
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
