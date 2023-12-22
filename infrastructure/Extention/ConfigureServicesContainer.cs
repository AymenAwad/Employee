using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Extention
{
    public static class ConfigureServicesContainer
    {
        //public static void AddDbContext(this IServiceCollection serviceCollection,
        //    IConfiguration configuration, IConfigurationRoot configRoot)
        //{
        //    serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        //           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") 
        //        , b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        //}
    }
}
