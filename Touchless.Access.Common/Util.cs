// =============================================================================
// Util.cs
// 
// Autor  : Felipe Bernardi
// Data   : 14/04/2022
// =============================================================================
namespace Touchless.Access.Common
{
    public static class Util
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Validar o CNPJ.
        /// </summary>
        /// <param name="cnpj">CNPJ a ser validado.</param>
        /// <returns>Verdadeiro se válido, caso contrário, falso.</returns>
        public static bool IsValidCnpj( string cnpj )
        {
            var multiplicador1 = new[] {5 , 4 , 3 , 2 , 9 , 8 , 7 , 6 , 5 , 4 , 3 , 2};
            var multiplicador2 = new[] {6 , 5 , 4 , 3 , 2 , 9 , 8 , 7 , 6 , 5 , 4 , 3 , 2};
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace( "." , "" ).Replace( "-" , "" ).Replace( "/" , "" );
            if( cnpj.Length != 14 ) return false;

            var tempCnpj = cnpj.Substring( 0 , 12 );
            var soma = 0;
            for( var index = 0 ; index < 12 ; index++ ) soma += int.Parse( tempCnpj[index].ToString() ) * multiplicador1[index];

            var resto = soma % 11;
            if( resto < 2 )
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for( var index = 0 ; index < 13 ; index++ ) soma += int.Parse( tempCnpj[index].ToString() ) * multiplicador2[index];

            resto = soma % 11;
            if( resto < 2 )
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto;
            return cnpj.EndsWith( digito );
        }

        /// <summary>
        /// Validar o CPF.
        /// </summary>
        /// <param name="cpf">CPF a ser validado.</param>
        /// <returns>Verdadeiro se válido, caso contrário, falso.</returns>
        public static bool IsValidCpf( string cpf )
        {
            var multiplicador1 = new[] {10 , 9 , 8 , 7 , 6 , 5 , 4 , 3 , 2};
            var multiplicador2 = new[] {11 , 10 , 9 , 8 , 7 , 6 , 5 , 4 , 3 , 2};
            cpf = cpf.Trim();
            cpf = cpf.Replace( "." , "" ).Replace( "-" , "" );
            if( cpf.Length != 11 ) return false;

            var tempCpf = cpf.Substring( 0 , 9 );
            var soma = 0;

            for( var index = 0 ; index < 9 ; index++ ) soma += int.Parse( tempCpf[index].ToString() ) * multiplicador1[index];

            var resto = soma % 11;
            if( resto < 2 )
                resto = 0;
            else
                resto = 11 - resto;

            var digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for( var index = 0 ; index < 10 ; index++ ) soma += int.Parse( tempCpf[index].ToString() ) * multiplicador2[index];

            resto = soma % 11;
            if( resto < 2 )
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto;
            return cpf.EndsWith( digito );
        }
        #endregion
    }
}