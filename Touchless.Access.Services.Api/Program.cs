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
    /// Classe respons�vel pela cria��o/configura��o da aplica��o.
    /// </summary>
    public static class Program
    {
        #region M�todos/Operadores P�blicos
        /// <summary>
        /// Criar o objeto que hospedar� a aplica��o.
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
        /// M�todo disparado quando houver um exce��o na tratada na aplica��o.
        /// </summary>
        /// <param name="sender">Ojeto que gerou a exce��o.</param>
        /// <param name="eventArgs">Argumento contendo as informa��es sobre a exce��o.</param>
        private static void OnUnhandledException( object sender , UnhandledExceptionEventArgs eventArgs )
        {
            if( eventArgs.ExceptionObject is System.Exception exception )
                Log.Fatal( $"Exce��o [{exception.Message}], detalhes: {exception.StackTrace} " );
            else
                Log.Fatal( "Um erro n�o tratado ocorreu na aplica��o" );
        }

        /// <summary>
        /// Fun��o principal da aplica��o.
        /// </summary>
        public static async Task Main( string[] args )
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
                var host = CreateHostBuilder( args ).Build().MigrateDatabase<ApplicationContext>();

                Log.Information( "Touchless Access API" );
                Log.Information( $"Iniciando o servi�o - Vers�o [{Assembly.GetEntryAssembly()?.GetName().Version}]" );

                await host.RunAsync().ConfigureAwait( false );
            }
            catch( OperationCanceledException )
            {
                // Ignorar
            }
            catch( System.Exception ex )
            {
                Log.Fatal( ex , "Aplica��o finalizada de forma inesperada" );
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