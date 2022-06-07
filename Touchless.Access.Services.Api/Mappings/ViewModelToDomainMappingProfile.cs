// =============================================================================
// ViewModelToDomainMappingProfile.cs
// 
// Autor  : Felipe Bernardi
// Data   : 24/05/2022
// =============================================================================

using AutoMapper;
using Touchless.Access.Data.Models;
using Touchless.Access.Services.Common.Models;

// ReSharper disable ClassNeverInstantiated.Global

namespace Touchless.Access.Services.Api.Mappings
{
    /// <summary>
    /// </summary>
    public class ViewModelToDomainMappingProfile : Profile
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public ViewModelToDomainMappingProfile()
        {
            

            #region AddressViewModel
            CreateMap<AddressViewModel , Address>();
            #endregion


            #region AuthenticationKeyViewModel
            CreateMap<AuthenticationKeyViewModel , AuthenticationKey>();
            #endregion


            #region ClientViewModel
            CreateMap<ClientViewModel , Client>();
            #endregion

           
            #region RoleViewModel
            CreateMap<RoleViewModel , Role>();
            #endregion

            #region TelephoneViewModel
            CreateMap<TelephoneViewModel , Telephone>();
            #endregion

            #region UserViewModel
            CreateMap<UserViewModel , User>();
            #endregion

            #region UserRoleViewModel
            CreateMap<UserRoleViewModel , UserRole>();
            #endregion

        }
        #endregion
    }
}