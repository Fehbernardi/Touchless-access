// =============================================================================
// Startup.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================

using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Touchless.Access.Authentication;
using Touchless.Access.Authentication.Models;
using Touchless.Access.Data;
using Touchless.Access.Repository;
using Touchless.Access.Repository.Interfaces;
using Touchless.Access.Services.Api.Mappings;
using Touchless.Access.Services.Common.Models;
using Touchless.Access.Services.Common.Validators;
using Touchless.Access.Services.Interfaces;

// ReSharper disable MemberCanBePrivate.Global

namespace Touchless.Access.Services.Api
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        #region Variáveis Estáticas
        private static readonly ILoggerFactory ZylixLoggerFactory
            = LoggerFactory.Create( builder =>
            {
                builder
                    .AddFilter( ( category , level ) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information )
                    .AddConsole();
            } );
        #endregion

        #region Propriedades Públicas
        /// <summary>
        /// Recuperar objeto contendo as configurações.
        /// </summary>
        public IConfiguration Configuration{ get; }

        /// <summary>
        /// Recuperar objeto contendo informações sobre o ambiente.
        /// </summary>
        public IWebHostEnvironment Environment{ get; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="environment">Ambiente utilizado.</param>
        public Startup( IWebHostEnvironment environment )
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath( Directory.GetCurrentDirectory() )
                .AddJsonFile( "appsettings.json" , true , false )
                .AddJsonFile( $"appsettings.{environment.EnvironmentName}.json" , true , false )
                .AddKeyPerFile( Path.Combine( Directory.GetCurrentDirectory() , "configuration" ) , true )
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Configurar a aplicação.
        /// </summary>
        /// <param name="application">Objeto de criação da aplicação.</param>
        /// <param name="environment">Ambiente utilizado.</param>
        public void Configure( IApplicationBuilder application , IWebHostEnvironment environment )
        {
            #region Cabeçalhos Segurança
            var policyCollection = new HeaderPolicyCollection()
                .AddDefaultSecurityHeaders()
                .RemoveServerHeader();

            application.UseSecurityHeaders( policyCollection );
            #endregion

            #region Logging/Debug/Hsts
            if( environment.IsDevelopment() ) application.UseDeveloperExceptionPage();
            #endregion

            #region Compressão Resposta
            application.UseResponseCompression();
            #endregion

            #region Cache Resposta
            application.UseResponseCaching();
            #endregion

            #region Documentação WebApi
            application.UseSwagger( option =>
            {
                option.PreSerializeFilters.Add( ( swagger , httpReq ) =>
                {
                    swagger.Servers = new List<OpenApiServer>
                    {
                        new() { Url = $"https://{httpReq.Host.Value}" } ,
                        new() { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" }
                    };
                } );
            } );
            application.UseSwaggerUI( option =>
            {
                option.SwaggerEndpoint( "/swagger/v1/swagger.json" , "Touchless Access API - v1" );

                option.InjectStylesheet( "/swagger-ui/custom.css" );
                option.InjectJavascript( "/swagger-ui/custom.js" );
                option.DocumentTitle = "Touchless Access API - v1";

                option.DisplayRequestDuration();
            } );
            #endregion

            #region Autenticação JWT
            application.ConfigureJwtAuthentication();
            #endregion

            #region Geral
            application.UseHttpsRedirection();
            application.UseDefaultFiles();
            application.UseStaticFiles();
            application.UseRouting();
            application.UseSerilogRequestLogging();

            application.UseCors("PolicyAccess");
            application.UseAuthentication();
            application.UseAuthorization();
            
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHealthChecks("/api/v1/healthcheck");
            });

            #endregion
        }

        /// <summary>
        /// Configurar os serviços.
        /// </summary>
        /// <param name="services">Coleção de serviços.</param>
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddHealthChecks()
                .AddNpgSql(Configuration["ACCESS_SERVICE_DB"],
                    tags : new [] { "postgres" },
                    name: "postgres", 
                    failureStatus: HealthStatus.Unhealthy);

            #region Compressão Resposta
            services.Configure<BrotliCompressionProviderOptions>( options => options.Level = CompressionLevel.Fastest );
            services.Configure<GzipCompressionProviderOptions>( options => options.Level = CompressionLevel.Fastest );
            services.AddResponseCompression( options =>
            {
                options.EnableForHttps = true;

                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            } );
            #endregion

            #region Cache Resposta
            services.AddResponseCaching();
            #endregion

            #region Limitação Taxa IP
            services.AddOptions();
            services.AddDistributedMemoryCache();
            #endregion

            #region Redis
            var redisConfiguration = new RedisConfiguration
            {
                Password = Configuration["REDIS_PASSWORD"] ,
                AbortOnConnectFail = true ,
                KeyPrefix = "ZYLIX_" ,
                AllowAdmin = true ,
                ConnectTimeout = 6000 ,
                Database = 0 ,
                ServerEnumerationStrategy = new ServerEnumerationStrategy
                {
                    Mode = ServerEnumerationStrategy.ModeOptions.All ,
                    TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any ,
                    UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
                } ,
                PoolSize = 50 ,
                Ssl = false ,
                Hosts = new[]
                {
                    new RedisHost
                    {
                        Host = Configuration["REDIS_SERVER"] ,
                        Port = Convert.ToInt32( Configuration["REDIS_SERVER_PORT"] )
                    }
                }
            };
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>( redisConfiguration );
            #endregion

            #region Injeção Dependência

                
            services.AddDbContext<ApplicationContext>( options =>
            {
                options.UseNpgsql( Configuration["ACCESS_SERVICE_DB"] , optionsAction => optionsAction.UseQuerySplittingBehavior( QuerySplittingBehavior.SingleQuery ) )
#if(DEBUG)
                    .UseLoggerFactory( ZylixLoggerFactory )
#endif
                    .EnableSensitiveDataLogging( Environment.IsDevelopment() )
                    .UseQueryTrackingBehavior( QueryTrackingBehavior.NoTracking );
            } , ServiceLifetime.Transient );

            services.AddHttpContextAccessor();
            services.AddSingleton( Environment );

            var mapperConfiguration = new MapperConfiguration( configure =>
            {
                configure.AddProfile<DomainToViewModelMappingProfile>();
                configure.AddProfile<ViewModelToDomainMappingProfile>();
            } );

            services.AddSingleton( mapperConfiguration.CreateMapper() );
            services.AddSingleton<IHttpContextAccessor , HttpContextAccessor>();

            services.AddTransient<IAddressRepository , AddressRepository>();
            services.AddTransient<IAuthenticationKeyRepository , AuthenticationKeyRepository>();
            services.AddTransient<IClientRepository , ClientRepository>();
            services.AddTransient<IRoleRepository , RoleRepository>();
            services.AddTransient<ITelephoneRepository , TelephoneRepository>();
            services.AddTransient<IUserRepository , UserRepository>();

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthenticationKeyService , AuthenticationKeyService>();
            services.AddTransient<IClientService , ClientService>();
            services.AddTransient<IRoleService , RoleService>();
            services.AddTransient<IUserService , UserService>();

            services.AddTransient<IValidator<AddressViewModel> , AddressValidator>();
            services.AddTransient<IValidator<AuthenticationKeyViewModel> , AuthenticationKeyValidator>();
            services.AddTransient<IValidator<ChangePasswordRequestViewModel> , ChangePasswordRequestValidator>();
            services.AddTransient<IValidator<ClientViewModel> , ClientValidator>();
            services.AddTransient<IValidator<LoginRequestViewModel> , LoginRequestValidator>();
            services.AddTransient<IValidator<RoleViewModel> , RoleValidator>();
            services.AddTransient<IValidator<TelephoneViewModel> , TelephoneValidator>();
            services.AddTransient<IValidator<UserViewModel> , UserValidator>();
            services.AddTransient<IValidator<UserRoleViewModel> , UserRoleValidator>();
            #endregion

            #region Habilitar Cors
            services.AddCors( options =>
            {
                options.AddPolicy("PolicyAccess",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders( "X-Pagination" ) );
            } );
            #endregion

            #region SignalR
            services.AddSignalR( hubOptions =>
            {
                hubOptions.ClientTimeoutInterval = TimeSpan.FromSeconds( 3600 );
                hubOptions.HandshakeTimeout = TimeSpan.FromSeconds( 30 );
                hubOptions.KeepAliveInterval = TimeSpan.FromSeconds( 15 );
                hubOptions.EnableDetailedErrors = true;
                hubOptions.MaximumReceiveMessageSize = 32000;
                hubOptions.StreamBufferCapacity = 10;
            } ).AddMessagePackProtocol();
            #endregion

            #region Geral
            services.AddControllers()
                .AddNewtonsoftJson( options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    }
                )
                .AddFluentValidation( options =>
                {
                    options.DisableDataAnnotationsValidation = true;
                    options.ImplicitlyValidateChildProperties = true;
                } );
            #endregion

            #region Versionamento WebApi
            services.AddApiVersioning( option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = true;
                option.DefaultApiVersion = new ApiVersion( 1 , 0 );
            } );
            #endregion

            #region Documentação WebApi
            services.AddSwaggerGen( option =>
            {
                option.DescribeAllParametersInCamelCase();
                option.EnableAnnotations();

                option.SwaggerDoc( "v1" , new OpenApiInfo { Title = "Touchless Access API - v1" , Version = "v1" } );

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var directoryInfo = new DirectoryInfo( basePath );
                foreach( var file in directoryInfo.EnumerateFiles( "*.xml" ) ) option.IncludeXmlComments( file.FullName );

                #region API Key Authentication
                option.AddSecurityDefinition( "ApiKey" ,
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header ,
                        Description = string.Empty ,
                        Name = "access-api-key" ,
                        Type = SecuritySchemeType.ApiKey ,
                        Scheme = "ApiKey"
                    } );

                option.AddSecurityRequirement( new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme ,
                                Id = "ApiKey"
                            } ,
                            Scheme = "oauth2" ,
                            Name = "ApiKey" ,
                            In = ParameterLocation.Header
                        } ,
                        new List<string>()
                    }
                } );
                #endregion

                #region JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication" ,
                    Description = "Enter JWT Bearer token **_only_**" ,
                    In = ParameterLocation.Header ,
                    Type = SecuritySchemeType.Http ,
                    Scheme = "bearer" ,
                    BearerFormat = "JWT" ,
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme ,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                option.AddSecurityDefinition( securityScheme.Reference.Id , securityScheme );
                option.AddSecurityRequirement( new OpenApiSecurityRequirement
                {
                    { securityScheme , new string[] {} }
                } );
                #endregion
            } );

            services.AddSwaggerGenNewtonsoftSupport();
            #endregion

            #region Autenticação JWT
            services.AddJwtAuthentication( Configuration.GetSection( "jwtTokenConfig" ).Get<JwtTokenConfig>() );
            #endregion
        }
        #endregion
    }
}