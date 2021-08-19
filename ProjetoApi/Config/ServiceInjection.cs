using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Context;
using DataAccess.Interfaces;
using DataAccess.Repositories;

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


        }
    }
}
