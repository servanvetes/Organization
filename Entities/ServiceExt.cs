using Entities.Models;
using Entities.RepositoryClass;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Repository;

namespace Entities
{
    public static class ServiceExt
    {
        public static void AddSubService(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddDbContext<OrganizationContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DbMssql")));
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IActivityRepository, ActivityRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<ICityRepository, CityRepository>();
            service.AddScoped<ICityRepository, CityRepository>();
            service.AddScoped<ITicketCompanyRepository, TicketCompanyRepository>();
            service.AddScoped<IUserActivityRepository, UserActivityRepository>();
        }
    }
}
