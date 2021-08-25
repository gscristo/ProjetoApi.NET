using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Context;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Core.Interfaces;
using Core;
using Core.Users;

namespace Api.Config
{
    public class ServiceInjection
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection("DatabaseSettings");

            #region  DataAccess

            services.AddSingleton<IDataContext>(serviceProvider => new DataContext(databaseSettings["ProjetoApiSqlConnection"]));
            services.AddScoped<IUsersRepository, UsersRepository>();

            #endregion

            #region Core Services

            services.AddScoped<ICreateUsers, CreateUsers>();
            services.AddScoped<IUpdateUsers, UpdateUsers>();
            //services.AddScoped<IDeleteUsers, DeleteUsers>();
            services.AddScoped<IGetAllUsers, GetAllUsers>();

            #endregion

        }
    }
}
