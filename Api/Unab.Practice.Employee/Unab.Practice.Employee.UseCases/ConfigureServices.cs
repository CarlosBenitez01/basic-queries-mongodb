using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Unab.Practice.Employees.UseCases.Common.Mappings;

namespace Unab.Practice.Employees.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
