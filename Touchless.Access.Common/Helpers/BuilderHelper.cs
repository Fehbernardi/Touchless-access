// =============================================================================
// BuilderHelper.cs
// 
// Autor  : Felipe Bernardi
// Data   : 15/04/2022
// =============================================================================

using System;
using System.Globalization;
using System.Reflection;

namespace Touchless.Access.Common.Helpers
{
    /// <summary>
    /// Classe responsável pela inicialização dos objetos através de Reflection.
    /// </summary>
    public static class BuilderHelper
    {
        #region Métodos/Operadores Públicos
        /// <summary>
        /// Inicializar um determinado objeto através de Reflection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// Especifica o
        /// <see cref="Type" />
        /// do componente a ser inicializado.
        /// <param name="type"><see cref="Type" /> do objeto a ser criado.</param>
        /// <returns>Objeto do tipo especificado.</returns>
        /// <exception cref="ArgumentNullException" />
        public static T BuildUp<T>( Type type )
        {
            #region Validar parâmetros
            if( type == null ) throw new ArgumentNullException( nameof(type) );
            #endregion

            return (T) Activator.CreateInstance( type , true );
        }

        /// <summary>
        /// Inicializar um determinado objeto atravé de Reflecion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// Especifica o
        /// <see cref="Type" />
        /// do componente a ser inicializado.
        /// <param name="type"><see cref="Type" /> do objeto a ser criado.</param>
        /// <param name="parameters">Parâmetros utilizados na inicialização do componente.</param>
        /// <returns>Objeto do tipo especificado.</returns>
        /// <exception cref="ArgumentNullException" />
        public static T BuildUp<T>( Type type , object[] parameters )
        {
            #region Validar parâmetros
            if( type == null ) throw new ArgumentNullException( nameof(type) );

            if( parameters == null ) throw new ArgumentNullException( nameof(parameters) );
            #endregion

            return (T) Activator.CreateInstance( type , parameters );
        }

        /// <summary>
        /// Inicializar um determinado objeto através de Reflection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// Especifica o
        /// <see cref="Type" />
        /// do componente a ser inicializado.
        /// <param name="assemblyName">Nome do <see cref="Assembly" /> a ser criado.</param>
        /// <param name="typeName">Nome do <see cref="Type" /> do objeto a ser criado.</param>
        /// <returns>Objeto do tipo especificado.</returns>
        public static T BuildUp<T>( string assemblyName , string typeName )
        {
            #region Validar parâmetros
            if( string.IsNullOrEmpty( assemblyName ) ) throw new ArgumentNullException( nameof(assemblyName) );

            if( string.IsNullOrEmpty( typeName ) ) throw new ArgumentNullException( nameof(typeName) );
            #endregion

            var assembly = Assembly.LoadFrom( assemblyName );
            return (T) assembly.CreateInstance( typeName , true );
        }

        /// <summary>
        /// Inicializar um determinado objeto através de Reflection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// Especifica o
        /// <see cref="Type" />
        /// do componente a ser inicializado.
        /// <param name="assemblyName">Nome do <see cref="Assembly" /> a ser criado.</param>
        /// <param name="typeName">Nome do <see cref="Type" /> do objeto a ser criado.</param>
        /// <param name="parameters">Parâmetros utilizados na inicialização do componente.</param>
        /// <returns>Objeto do tipo especificado.</returns>
        public static T BuildUp<T>( string assemblyName , string typeName , object[] parameters )
        {
            #region Validar parâmetros
            if( string.IsNullOrEmpty( assemblyName ) ) throw new ArgumentNullException( nameof(assemblyName) );

            if( string.IsNullOrEmpty( typeName ) ) throw new ArgumentNullException( nameof(typeName) );

            if( parameters == null ) throw new ArgumentNullException( nameof(parameters) );
            #endregion

            var assembly = Assembly.LoadFrom( assemblyName );
            return (T) assembly.CreateInstance( typeName , true , BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance , null , parameters , null , null );
        }

        /// <summary>
        /// Inicializar um determinado objeto através de Reflection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// Especifica o
        /// <see cref="Type" />
        /// do componente a ser inicializado.
        /// <param name="assemblyName">Nome do <see cref="Assembly" /> a ser criado.</param>
        /// <param name="typeName">Nome do <see cref="Type" /> do objeto a ser criado.</param>
        /// <param name="parameters">Parâmetros utilizados na inicialização do componente.</param>
        /// <param name="culture">Objeto <see cref="CultureInfo" /> utilizado para governar a coerção dos tipos.</param>
        /// <returns>Objeto do tipo especificado.</returns>
        public static T BuildUp<T>( string assemblyName , string typeName , object[] parameters , CultureInfo culture )
        {
            #region Validar parâmetros
            if( string.IsNullOrEmpty( assemblyName ) ) throw new ArgumentNullException( nameof(assemblyName) );

            if( string.IsNullOrEmpty( typeName ) ) throw new ArgumentNullException( nameof(typeName) );

            if( parameters == null ) throw new ArgumentNullException( nameof(parameters) );

            if( culture == null ) throw new ArgumentNullException( nameof(culture) );
            #endregion

            var assembly = Assembly.LoadFrom( assemblyName );
            return (T) assembly.CreateInstance( typeName , true , BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance , null , parameters , culture , null );
        }

        /// <summary>
        /// Retornar o tipo de um determinado objeto.
        /// </summary>
        /// <param name="assemblyName">>Nome do <see cref="Assembly" /> a ser criado</param>
        /// <param name="typeName">Nome do <see cref="Type" /> do objeto a ser criado.</param>
        /// <returns>Tipo do objeto.</returns>
        public static Type GetType( string assemblyName , string typeName )
        {
            #region Validar parâmetros
            if( string.IsNullOrEmpty( assemblyName ) ) throw new ArgumentNullException( nameof(assemblyName) );

            if( string.IsNullOrEmpty( typeName ) ) throw new ArgumentNullException( nameof(typeName) );
            #endregion

            var assembly = Assembly.LoadFrom( assemblyName );
            return assembly.GetType( typeName );
        }

        /// <summary>
        /// Determinar se a classe representada pelo <paramref name="typeName">typeName</paramref> deriva
        /// da classe representada pelo <paramref name="instrumentedTypeName">instrumentedTypeName</paramref>.
        /// </summary>
        /// <param name="assemblyName">Nome do <see cref="Assembly" /> a ser criado.</param>
        /// <param name="typeName">Nome do <see cref="Type">Type</see> do objeto a ser verificado.</param>
        /// <param name="instrumentedTypeName">Nome do <see cref="Type">Type</see> a ser comparado.</param>
        /// <returns><b>Verdadeiro</b> se o tipo indicado herdar da classe base indicada.</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        public static bool IsSubclassOf( string assemblyName , string typeName , string instrumentedTypeName )
        {
            #region Validar parâmetros
            if( string.IsNullOrEmpty( assemblyName ) ) throw new ArgumentNullException( nameof(assemblyName) );

            if( string.IsNullOrEmpty( typeName ) ) throw new ArgumentNullException( nameof(typeName) );

            if( string.IsNullOrEmpty( instrumentedTypeName ) ) throw new ArgumentNullException( nameof(instrumentedTypeName) );
            #endregion

            var reflectedType = Assembly.LoadFrom( assemblyName ).GetType( typeName );
            var instrumentedType = Type.GetType( instrumentedTypeName );

            return IsSubclassOf( reflectedType , instrumentedType );
        }

        /// <summary>
        /// Determinar se a classe representada pelo <paramref name="reflectedType">reflectedType</paramref>
        /// deriva da classe representada pelo <paramref name="instrumentedType">instrumentedType</paramref>.
        /// </summary>
        /// <param name="reflectedType"><see cref="Type">Type</see> do objeto a ser verificado.</param>
        /// <param name="instrumentedType"><see cref="Type">Type</see> a ser comparado.</param>
        /// <returns><b>Verdadeiro</b> se o tipo indicado herdar da classe base indicada.</returns>
        /// <exception cref="ArgumentNullException" />
        /// <exception cref="ArgumentException" />
        public static bool IsSubclassOf( Type reflectedType , Type instrumentedType )
        {
            #region Validar parâmetros
            if( reflectedType == null ) throw new ArgumentNullException( nameof(reflectedType) );

            if( instrumentedType == null ) throw new ArgumentNullException( nameof(instrumentedType) );
            #endregion

            return reflectedType.IsSubclassOf( instrumentedType );
        }
        #endregion
    }
}