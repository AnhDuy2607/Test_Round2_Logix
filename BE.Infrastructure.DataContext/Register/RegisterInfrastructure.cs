using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BE.Infrastructure.DataContext.Register
{
    public static class RegisterInfrastructure
    {
        public static IServiceCollection RegisterMovieDataContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MovieDataContext>(opts =>
            {
                opts.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}