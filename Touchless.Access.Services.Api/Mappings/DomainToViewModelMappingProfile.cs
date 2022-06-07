// =============================================================================
// DomainToViewModelMappingProfile.cs
// 
// Autor  : Felipe Bernardi
// Data   : 24/05/2022
// =============================================================================
using AutoMapper;
using Touchless.Access.Data.Models;
using Touchless.Access.Pagination;
using Touchless.Access.Services.Common.Models;

namespace Touchless.Access.Services.Api.Mappings
{
    /// <summary>
    /// </summary>
    public class DomainToViewModelMappingProfile : Profile
    {
        #region Construtores
        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public DomainToViewModelMappingProfile()
        {
           

			#region Address
			CreateMap<Address , AddressViewModel>();
            #endregion

            
            #region Customer
            CreateMap<Client , ClientViewModel>();
            CreateMap<PagedList<Client> , PagedList<ClientViewModel>>()
                .ConvertUsing<PagedListConverter<Client, ClientViewModel>>();
            #endregion
                       
            #region Role
            CreateMap<Role , RoleViewModel>();
            CreateMap<PagedList<Role> , PagedList<RoleViewModel>>()
                .ConvertUsing<PagedListConverter<Role , RoleViewModel>>();
			#endregion

			#region Telephone
			CreateMap<Telephone , TelephoneViewModel>();
            CreateMap<User , UserViewModel>();
			#endregion
            
            #region User
            CreateMap<PagedList<User> , PagedList<UserViewModel>>()
                .ConvertUsing<PagedListConverter<User , UserViewModel>>();
            CreateMap<UserRole , UserRoleViewModel>();
            #endregion
        }
        #endregion
    }
}