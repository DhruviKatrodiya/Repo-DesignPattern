namespace CollegeApplication.API.Configurations
{
    public static class CorsConfiguration
    {
        public static void AddCors(this IApplicationBuilder app)
        {
            app.UseCors("default");
        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            //Add service and create policy with options
            services.AddCors(options =>
            {
                options.AddPolicy("default",
                    builder =>
                    {
                        builder.WithOrigins("*")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
                    });
            });
        }
    }
}
