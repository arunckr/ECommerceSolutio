using ECommerceApplication.Interfaces;
using ECommerceApplication.Queries;
using System.Reflection;

namespace ECommerceApi
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }

        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddScoped<ICustomerOrderQueries, CustomerOrderQueries>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
        private static void RegisterTypesEndingWithQueries(IServiceCollection services, Assembly assembly)
        {
            var queryTypes = assembly.GetTypes()
                .Where(type => type.Name.EndsWith("Queries"))
                .ToList();

            foreach (var queryType in queryTypes)
            {
                // Register the type with a scoped lifetime. You can change the lifetime as needed.
                services.AddScoped(queryType);
            }

        }

    }
}
