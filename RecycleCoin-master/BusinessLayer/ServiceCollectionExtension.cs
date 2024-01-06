using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserManager>();
            services.AddSingleton<IProductService, ProductManager>();
        }
    }
}
