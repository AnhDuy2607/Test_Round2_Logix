using BE.UnitOfWork.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.UnitOfWork.Register
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            return services;
        }
    }
}
