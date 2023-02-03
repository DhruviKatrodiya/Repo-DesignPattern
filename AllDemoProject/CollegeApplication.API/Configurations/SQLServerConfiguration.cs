using CollegeApplication.Infra.Domain;
using Microsoft.EntityFrameworkCore;

namespace CollegeApplication.API.Configurations
{
    public static class SQLServerConfiguration
    {
        public static void AddSqlServer(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:DBConnection"];
            services.AddDbContext<CollegeApplicationContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(connectionString, x =>
                 {
                     x.MigrationsAssembly("CollegeApplication.Infra.Domain");
                     x.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                 });
            }, ServiceLifetime.Singleton);
        }
    }
}
