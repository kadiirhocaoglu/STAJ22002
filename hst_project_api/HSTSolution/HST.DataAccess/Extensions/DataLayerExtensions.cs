
using HST.DataAccess.Concrete;
using HST.DataAccess.Repositories.Abstract;
using HST.DataAccess.Repositories.Concrete;
using HST.DataAccess.UnitOfWork.Abstact;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HST.DataAccess.Extensions
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<Context>(opt => opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork.Concrete.UnitOfWork>();
            return services;
        }
    }



}

