// =============================================================================
// PasswordHelper.cs
// 
// Autor  : Felipe Bernardi
// Data   : 15/05/2022
// =============================================================================

using System;
using System.Text.RegularExpressions;

namespace Touchless.Access.Common.Helpers
{
    public static class PasswordHelper
    {
        #region Variáveis Estáticas
        private static readonly Regex _passwordRegex = new( "((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,})" );
        #endregion
        
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Verificar se o senha é válida.
        /// </summary>
        /// <param name="password">Senha.</param>
        /// <returns>Verdadeiro se a senha for válida, caso contrário, falso.</returns>
        public static bool IsValid( string password )
        {
            try
            {
                return password != null && _passwordRegex.IsMatch( password );
            }
            catch( ArgumentException )
            {
                return false;
            }
        }
        #endregion
    }
}