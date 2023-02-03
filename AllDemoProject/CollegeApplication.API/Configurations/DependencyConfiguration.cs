using CollegeApplication.Core.Contract;
using CollegeApplication.Core.Service;
using CollegeApplication.Core.Service.Helper;
using CollegeApplication.Infra.Contract;
using CollegeApplication.Infra.Repository;

namespace CollegeApplication.API.Configurations
{
    public static class DependencyConfiguration
    {
        public static void AddDependency(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddTransient<FileUploadHelper>();
        }
    }
}
