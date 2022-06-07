// =============================================================================
// DuplicateResourceException.cs
// 
// Autor  : Felipe Bernardi
// Data   : 28/04/2022
// =============================================================================

namespace Touchless.Access.Exception
{
    /// <summary>
    /// Exceção para identificar que um recurso é duplicado.
    /// </summary>
    public class DuplicateResourceException : BaseException
    {
        #region Propriedades Públicas
        /// <summary>
        /// Atribuir/Recuperar o nome do recurso duplicado.
        /// </summary>
        public string ResourceName{ get; set; }
        #endregion

        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="resourceName">Nome do campo.</param>
        /// <param name="userFriendlyMessage">Mensagem amigável descrevendo o erro.</param>
        public DuplicateResourceException( string resourceName , string userFriendlyMessage ) : base( userFriendlyMessage )
        {
            ResourceName = resourceName;
        }
        #endregion
    }
}