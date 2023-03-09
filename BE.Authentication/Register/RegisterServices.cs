using BE.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Authentication.Register
{
    public static class RegisterServices
    {
        public static IServiceCollection RegisterAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(ConstantValue.DEFAULT_SCHEMA_AUTHENTICATION)
            .AddScheme<BasicAuthenticationOptions, AuthenticationCustomize>(ConstantValue.DEFAULT_SCHEMA_AUTHENTICATION, null);
            return services;
        }
    }
}
