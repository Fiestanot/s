using Microsoft.Extensions.DependencyInjection;
using OrderApi.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.ServiceExtension
{
    public static class ServiceExtension
    {
        public static void ConfigureRepositories(this IServiceCollection services, string connectionString)
        {
            string BaseDBName = "OrderDB";

            services.AddSingleton(x => new OrderRepository(connectionString, BaseDBName, "order"));
        }
    }
}
