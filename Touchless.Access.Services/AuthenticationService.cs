// =============================================================================
// AuthenticationService.cs
// 
// Autor  : Felipe Bernardi
// Data   : 13/05/2022
// =============================================================================

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Touchless.Access.Services.Interfaces;
using Touchless.Access.Exception;
using Touchless.Access.Pagination;
using Touchless.Access.Services.Common;
using Touchless.Access.Services.Common.Models;
using Touchless.Access.Authentication.Interfaces;
using Touchless.Access.Api.Security;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Microsoft.AspNetCore.Http;

// ReSharper disable NotAccessedField.Local

namespace Touchless.Access.Services
{
    public class AuthenticationService : ServiceBase<AuthenticationService>, IAuthenticationService
    {
        #region Variáveis
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IUserService _userService;
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="redisCacheClient">Objeto responsável pelo gerenciamento do cache Redis.</param>
        /// <param name="jwtAuthenticationManager">Objeto responsável pelo gerenciamento da autenticação dos usuários.</param>
        /// <param name="userService">Objeto responsável pelas operaçôes relacionadas aos usuários.</param>
        /// <param name="httpContextAccessor">Objeto que prove acesso ao Contexto HTTP, se disponível.</param>
        public AuthenticationService( ILoggerFactory loggerFactory ,
            IRedisCacheClient redisCacheClient ,
            IJwtAuthenticationManager jwtAuthenticationManager ,
            IUserService userService ,
            IHttpContextAccessor httpContextAccessor , 
            IConfiguration configuration ) : base( loggerFactory, configuration )
        {
            _redisCacheClient = redisCacheClient ?? throw new ArgumentNullException( nameof(redisCacheClient) );
            _jwtAuthenticationManager = jwtAuthenticationManager ?? throw new ArgumentNullException( nameof(jwtAuthenticationManager) );
            _userService = userService ?? throw new ArgumentNullException( nameof(userService) );

            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException( nameof(httpContextAccessor) );
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Realizar o mudança da senha do usuário logado.
        /// </summary>
        /// <param name="changePasswordRequestViewModel">Objeto contendo as informações da novo senha</param>
        /// <returns>Resultado da operação.</returns>
        public async Task ChangePasswordAsync( ChangePasswordRequestViewModel changePasswordRequestViewModel )
        {
            var claim = _httpContextAccessor.HttpContext?.User.Claims!.FirstOrDefault( x => x.Type == ClaimTypes.Sid );
            if( claim != null )
            {
                var user = (await _userService.SearchAsync( new UserSearch {Id = Convert.ToInt32( claim.Value )} , new ResourceParameters() )).FirstOrDefault();
                if( user != null )
                {
                    if(!await _userService.IsValidUserCredentialsAsync( user.Username , changePasswordRequestViewModel.OldPassword))
                        throw new NotFoundException( "Senha do usuário não corresponde.");
                    user.Password = changePasswordRequestViewModel.NewPassword;
                    user.PasswordHash = PasswordCryptoHelper.GeneratePasswordHash( $"{user.Username}@{user.Password}" );
                    await _userService.UpdateAsync( user ).ConfigureAwait( false );
                }
                else
                {
                    throw new NotFoundException( "Usuário não localizado." );
                }
            }
            else
            {
                throw new UnauthorizedException( "Usuário não autorizado." );
            }
        }

        /// <summary>
        /// Realizar o login de um determinado usuário.
        /// </summary>
        /// <param name="loginRequestViewModel">Objeto contendo as informações do login.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<LoginResultViewModel> LoginAsync( LoginRequestViewModel loginRequestViewModel )
        {
            if( !await _userService.IsValidUserCredentialsAsync( loginRequestViewModel.UserName , loginRequestViewModel.Password ) )
            {
                throw new UnauthorizedException( "Usuário não autorizado." );
            }

            var user = (await _userService.SearchAsync( new UserSearch {Username = loginRequestViewModel.UserName} , new ResourceParameters() )
                .ConfigureAwait( false )).FirstOrDefault();
            if( user == null )
            {
                throw new UnauthorizedException( "Usuário não autorizado." );
            }

            var roles = (from userRole in user.UserRoles select userRole.Role.Name).ToList();

            var claims = new[]
            {
                new Claim( ClaimTypes.Sid , user.Id.ToString() ) ,
                new Claim( ClaimTypes.Name , loginRequestViewModel.UserName ) ,
                new Claim( ClaimTypes.Role , string.Join( "," , roles ) )
            };

            var jwtResult = await _jwtAuthenticationManager.GenerateTokensAsync( loginRequestViewModel.UserName , claims ).ConfigureAwait( false );
            return new LoginResultViewModel
            {
                UserName = loginRequestViewModel.UserName ,
                Role = string.Join( "," , roles ) ,
                AccessToken = jwtResult.AccessToken ,
                RefreshToken = jwtResult.RefreshToken.TokenString
            };
        }

        /// <summary>
        /// Realizar o logout do usuário.
        /// </summary>
        /// <param name="userName">Usuário logado.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task LogoutAsync( string userName )
        {
            await _jwtAuthenticationManager.RemoveRefreshTokenByUserNameAsync( userName ).ConfigureAwait( false );
        }

        /// <summary>
        /// Renovar o token de segurança.
        /// </summary>
        /// <param name="userName">Usuário logado.</param>
        /// <param name="accessToken">Token de acesso.</param>
        /// <param name="claim">Perfil do usuário.</param>
        /// <param name="refreshTokenRequestViewModel">Objeto contendo as informações para a renovação do token.</param>
        /// <returns>Resultado da operação.</returns>
        public async Task<LoginResultViewModel> RefreshTokenAsync( string userName , string accessToken , string claim , RefreshTokenRequestViewModel refreshTokenRequestViewModel )
        {
            var jwtResult = await _jwtAuthenticationManager.RefreshAsync( refreshTokenRequestViewModel.RefreshToken , accessToken ).ConfigureAwait( false );
            return new LoginResultViewModel
            {
                UserName = userName ,
                Role = claim ,
                AccessToken = jwtResult.AccessToken ,
                RefreshToken = jwtResult.RefreshToken.TokenString
            };
        }
        #endregion
    }
}