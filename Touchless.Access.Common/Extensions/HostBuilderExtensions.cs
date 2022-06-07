// =============================================================================
// HostBuilderExtensions.cs
// 
// Autor  : Felipe Bernardi
// Data   : 17/04/2022
// =============================================================================

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Touchless.Access.Common.Extensions
{
    public static class HostBuilderExtensions
    {
        #region Constantes
        private const string ConfigureServicesMethodName = "ConfigureServices";
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Aplicar as atualizações do modelo de banco de dados.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto.</typeparam>
        /// <param name="webHost">Objeto responsável pela inicialização da aplicação.</param>
        /// <returns>Resultado da operação.</returns>
        public static IHost MigrateDatabase<T>( this IHost webHost ) where T : DbContext
        {
            using var scope = webHost.Services.CreateScope();
            var services = scope.ServiceProvider;

            var databaseContext = services.GetRequiredService<T>();
            databaseContext.Database.Migrate();

            return webHost;
        }

        /// <summary>
        /// Utilizar um componente para inicialização da aplicação.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto.</typeparam>
        /// <param name="hostBuilder">Objeto responsável pela inicialização da aplicação.</param>
        /// <returns>>Objeto responsável pela inicialização da aplicação.</returns>
        public static IHostBuilder UseStartup<T>( this IHostBuilder hostBuilder ) where T : class
        {
            hostBuilder.ConfigureServices( ( ctx , serviceCollection ) =>
            {
                var cfgServicesMethod = typeof( T ).GetMethod( ConfigureServicesMethodName , new[] { typeof( IServiceCollection ) } );
                var hasConfigCtor = typeof( T ).GetConstructor( new[] { typeof( IConfiguration ) } ) != null;
                var startUpObj = hasConfigCtor ? (T)Activator.CreateInstance( typeof( T ) , ctx.Configuration ) : (T)Activator.CreateInstance( typeof( T ) , null );

                cfgServicesMethod?.Invoke( startUpObj , new object[] { serviceCollection } );
            } );

            return hostBuilder;
        }
        #endregion
    }
}