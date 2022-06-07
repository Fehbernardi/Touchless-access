// =============================================================================
// DirectoryHelper.cs
// 
// Autor  : Felipe Bernardi
// Data   : 23/05/2022
// =============================================================================

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Touchless.Access.Common.Helpers
{
    public static class DirectoryHelper
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Retornar o diretório corrente do executável.
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDirectory()
        {
            var location = Assembly.GetEntryAssembly()?.Location;

            if( RuntimeInformation.IsOSPlatform( OSPlatform.Windows ) ) return Path.GetDirectoryName( location ) + "\\";

            return Path.GetDirectoryName( location ) + "/";
        }
        #endregion
    }
}