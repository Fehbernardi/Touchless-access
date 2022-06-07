// =============================================================================
// UserService.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Transactions;
using Touchless.Access.Api.Security;
using Touchless.Access.Exception;
using Touchless.Access.Pagination;
using Touchless.Access.Repository.Interfaces;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;
using Touchless.Access.Services.Interfaces;

// ReSharper disable NotAccessedField.Local

namespace Touchless.Access.Services
{
    public partial class UserService : ServiceBase<UserService>, IUserService
    {
        #region Variáveis
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="redisCacheClient">Objeto responsável pelo gerenciamento do cache Redis.</param>
        /// <param name="userRepository">Objeto responsável pelas operaçôes relacionadas aos usuários.</param>
        /// <param name="roleRepository">Objeto responsável pelas operações relacionadas as funções.</param>
        /// <param name="httpContextAccessor">Objeto que prove acesso ao Contexto HTTP, se disponível.</param>
        public UserService( ILoggerFactory loggerFactory ,
            IRedisCacheClient redisCacheClient ,
            IUserRepository userRepository ,
            IRoleRepository roleRepository ,
            IHttpContextAccessor httpContextAccessor ,
            IConfiguration configuration ) : base( loggerFactory, configuration )
        {
            _redisCacheClient = redisCacheClient ?? throw new ArgumentNullException( nameof(redisCacheClient) );
            _userRepository = userRepository ?? throw new ArgumentNullException( nameof(userRepository) );
            _roleRepository = roleRepository ?? throw new ArgumentNullException( nameof(roleRepository) );

            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException( nameof(httpContextAccessor) );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Adicionar um novo usuário.
        /// </summary>
        /// <param name="user">Objeto contendo as informações do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<UserViewModel> AddAsync( UserViewModel user )
        {
            var users = await _userRepository.SearchAsync(
                    new UserSearch
                    {
                        Username = user.Username
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( users.Any() ) throw new DuplicateResourceException( "NOME" , "Já existe um usuário cadastrado com esse nome." );

            user.CreatedAt = DateTimeOffset.UtcNow;
            user.PasswordHash = PasswordCryptoHelper.GeneratePasswordHash( $"{user.Username}@{user.Password}" );

            var userRoles = user.UserRoles;
            user.UserRoles = null;

            using var transactionScope = new TransactionScope( TransactionScopeOption.RequiresNew , TransactionScopeAsyncFlowOption.Enabled );
            var newUser = await _userRepository.AddAsync( user ).ConfigureAwait( false );

            foreach( var userRole in userRoles ) newUser.UserRoles.Add( await _userRepository.AddUserRoleAync( newUser.Id , userRole.RoleId ).ConfigureAwait( false ) );

            transactionScope.Complete();

            return newUser;
        }

        /// <summary>
        /// Validar se as credenciais do usuário são válidas.
        /// </summary>
        /// <param name="userName">Usuário que esta querendo se autenticar.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> IsValidUserCredentialsAsync( string userName , string password )
        {
            var user = (await _userRepository.SearchAsync( new UserSearch
                {
                    Username = userName
                } , new ResourceParameters() )
                .ConfigureAwait( false )).FirstOrDefault();

            return user is { Active: true } && PasswordCryptoHelper.VerifyPassword( $"{userName}@{password}" , user.PasswordHash );
        }

        /// <summary>
        /// Retornar o(s) usuário(s) que atenda(m) o(s) filtro(s) especificado(s).
        /// </summary>
        /// <param name="search">Objeto utilizado para especificar os argumentos de busca dos usuários.</param>
        /// <param name="parameters">Objeto utilizado para especificar os parâmetros da paginação dos dados.</param>
        /// <returns>Coleção de usuários.</returns>
        public async Task<PagedList<UserViewModel>> SearchAsync( UserSearch search , ResourceParameters parameters )
        {
            return await _userRepository.SearchAsync( search , parameters ).ConfigureAwait( false );
        }

        /// <summary>
        /// Atualizar um determindo usuário.
        /// </summary>
        /// <param name="user">Objeto contendo as informações do usuário.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<bool> UpdateAsync( UserViewModel user )
        {
            var users = await _userRepository.SearchAsync(
                    new UserSearch
                    {
                        Id = user.Id
                    } , new ResourceParameters() )
                .ConfigureAwait( false );

            if( !users.Any() ) throw new NotFoundException( "Usuário não localizado." );

            // Verificar se é Admin
            var isAdmin = _httpContextAccessor.HttpContext?.User.Claims!.FirstOrDefault( x => x.Type == ClaimTypes.Role && x.Value.Contains( "admin" ) );
            if( isAdmin != null && !string.IsNullOrWhiteSpace( user.Password ) )
            {
                user.PasswordHash = PasswordCryptoHelper.GeneratePasswordHash( $"{user.Username}@{user.Password}" );
            }
            else
            {
                user.PasswordHash = users.FirstOrDefault()!.PasswordHash;
            }

            return await _userRepository.UpdateAsync( user ).ConfigureAwait( false );
        }
        #endregion
    }
}