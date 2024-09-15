using AutoMapper;
using HST.Business.Services.Abstract;
using HST.Business.Services.Concrete;
using HST.Business.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace HST.Business.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services, IConfiguration config)
        {

            var assembly = Assembly.GetExecutingAssembly();
            services.AddScoped<ICustomerAccountService, CustomerAccountService>();
            services.AddScoped<ICustomerAccountProcessService, CustomerAccountProcessService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
                (options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,

                        ValidIssuer = config["Token:Issuer"],
                        ValidAudience = config["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:SecurityKey"])),
                        ClockSkew = TimeSpan.Zero,
                    };
                   }
                    );
            services.AddAutoMapper(assembly);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;

        }
    }
}
