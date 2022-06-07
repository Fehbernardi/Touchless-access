// =============================================================================
// Program.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Touchless.Access.Data;
using Touchless.Access.Common.Extensions;

namespace Touchless.Access.Services.Api
{
    /// <summary>
    /// Classe responsável pela criação/configuração da aplicação.
    /// </summary>
    public static class Program
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Criar o objeto que hospedará a aplicação.
        /// </summary>
        /// <returns>Interface do objeto criado.</returns>
        private static IHostBuilder CreateHostBuilder( string[] args )
        {
            return Host.CreateDefaultBuilder( args )
                .ConfigureWebHostDefaults( webBuilder =>
                {
                    webBuilder.UseKestrel( options => { options.AddServerHeader = false; } );
                    webBuilder.UseContentRoot( Directory.GetCurrentDirectory() );
                    webBuilder.ConfigureLogging( builder =>
                    {
                        builder.ClearProviders();
                        builder.AddSerilog();
                    } );
#if(DEBUG)
                    webBuilder.UseUrls( "http://0.0.0.0:5050" );
#endif
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog( ( context , configuration ) => { configuration.ReadFrom.Configuration( context.Configuration ); } );
                } );
        }

        /// <summary>
        /// Método disparado quando houver um exceção na tratada na aplicação.
        /// </summary>
        /// <param name="sender">Ojeto que gerou a exceção.</param>
        /// <param name="eventArgs">Argumento contendo as informações sobre a exceção.</param>
        private static void OnUnhandledException( object sender , UnhandledExceptionEventArgs eventArgs )
        {
            if( eventArgs.ExceptionObject is System.Exception exception )
                Log.Fatal( $"Exceção [{exception.Message}], detalhes: {exception.StackTrace} " );
            else
                Log.Fatal( "Um erro não tratado ocorreu na aplicação" );
        }

        /// <summary>
        /// Função principal da aplicação.
        /// </summary>
        public static async Task Main( string[] args )
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
                var host = CreateHostBuilder( args ).Build().MigrateDatabase<ApplicationContext>();

                Log.Information( "Touchless Access API" );
                Log.Information( $"Iniciando o serviço - Versão [{Assembly.GetEntryAssembly()?.GetName().Version}]" );

                await host.RunAsync().ConfigureAwait( false );
            }
            catch( OperationCanceledException )
            {
                // Ignorar
            }
            catch( System.Exception ex )
            {
                Log.Fatal( ex , "Aplicação finalizada de forma inesperada" );
                throw;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        #endregion
    }
}