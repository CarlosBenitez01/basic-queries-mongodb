using Microsoft.Extensions.DependencyInjection;
using Unab.Practice.Employees.Interfaces.Persistence;
using Unab.Practice.Employees.Persistence.Contexts;
using Unab.Practice.Employees.Persistence.Repositories;

namespace Unab.Practice.Employees.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<MongoContext>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
