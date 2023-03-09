using BE.Core.Interfaces;
using BE.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Core.Register
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountServices, AccountServices>();
            services.AddTransient<IMovieServices, MovieServices>();
            services.AddTransient<ILikeHistoryServices, LikeHistoryServices>();
            return services;
        }
    }
}
