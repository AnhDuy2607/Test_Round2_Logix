using BE.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Repository.Register
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
