using CustomerApi.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerApi.ServiceExtension
{
    public static class ServiceExtension
    {
        public static void ConfigureRepositories(this IServiceCollection services, string connectionString)
        {
            string BaseDBName = "CustomerDB";

            services.AddSingleton(x => new CustomerRepository(connectionString, BaseDBName, "customer"));
        }
    }
}
