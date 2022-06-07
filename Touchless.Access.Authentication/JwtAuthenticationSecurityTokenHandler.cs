// =============================================================================
// JwtAuthenticationSecurityTokenHandler.cs
// 
// Autor  : Felipe Bernardi
// Data   : 19/04/2022
// =============================================================================

using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Touchless.Access.Authentication
{
    /// <summary>
    /// </summary>
    public class JwtAuthenticationSecurityTokenHandler : ISecurityTokenValidator
    {
        #region Variáveis
        private readonly JwtSecurityTokenHandler _tokenHandler;
        #endregion

        #region Propriedades Públicas
        /// <summary>
        /// Recuperar indicativo se o token pode ser validado.
        /// </summary>
        public bool CanValidateToken{ get; }

        /// <summary>
        /// Atribuir/Recuperar o tamanho máximo em bytes que o token de acesso pode ter.
        /// </summary>
        public int MaximumTokenSizeInBytes{ get; set; }

        /// <summary>
        /// Atribuir/Recuperar a função utilizada para validar o token de acesso.
        /// </summary>
        public Func<string , string , Task<bool>> ValidateAccessToken{ get; set; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtror padrão.
        /// </summary>
        public JwtAuthenticationSecurityTokenHandler()
        {
            MaximumTokenSizeInBytes = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;
            _tokenHandler = new JwtSecurityTokenHandler();
        }
        #endregion

        #region Métodos/Operadores Públicos
        /// <summary>
        /// Verificar se o token de acesso pode ser lido.
        /// </summary>
        /// <param name="securityToken">Token de segurança.</param>
        /// <returns></returns>
        public bool CanReadToken( string securityToken )
        {
            return _tokenHandler.CanReadToken( securityToken );
        }

        /// <summary>
        /// Validar o token de acesso.
        /// </summary>
        /// <param name="securityToken">Token de segurança.</param>
        /// <param name="validationParameters">Objeto contendo os parâmetros da validação.</param>
        /// <param name="validatedToken">Objeto contendo os dados do token de acesso.</param>
        /// <returns>Resultado da operação.</returns>
        public ClaimsPrincipal ValidateToken( string securityToken , TokenValidationParameters validationParameters , out SecurityToken validatedToken )
        {
            #region Validar parâmetros
            if( string.IsNullOrWhiteSpace( securityToken ) ) throw new ArgumentNullException( nameof(securityToken) );

            if( validationParameters == null ) throw new ArgumentNullException( nameof(validationParameters) );
            #endregion

            var principal = _tokenHandler.ValidateToken( securityToken , validationParameters , out validatedToken );

            var token = _tokenHandler.ReadJwtToken( securityToken );
            var isValid = ValidateAccessToken( token.Claims.First( x => x.Type == ClaimTypes.Name ).Value , securityToken ).GetAwaiter().GetResult();
            if( !isValid ) throw new SecurityTokenException( "Token inválido" );

            return principal;
        }
        #endregion
    }
}