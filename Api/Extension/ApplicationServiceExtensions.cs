using Api.Data;
using Api.Helpers;
using Api.Interfaces;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extension
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplictionServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddScoped<ITokenservice,Tokenservece>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddAutoMapper(typeof(AutoMaperProfiles).Assembly);
            services.AddDbContext<DataContext>(options=>{
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;

        }
        
    }
}