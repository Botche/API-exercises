namespace LibraryAPI.Infrastructure.AutoMapperProfiles
{
	using AutoMapper;

	using LibraryAPI.Database.Models.Users;
	using LibraryAPI.DTOs.Role;
	using LibraryAPI.DTOs.User;

	public class UserProfile : Profile
	{
		public UserProfile()
		{
			this.CreateMap<PostUserLoginDTO, User>();
			this.CreateMap<PostUserRegisterDTO, User>();
			this.CreateMap<User, GetUserInformationDTO>();
			this.CreateMap<User, GetUserIdDTO>();
			this.CreateMap<User, GetUserForSessionDTO>()
				.ForMember(gufsd => gufsd.Roles, x => x.MapFrom(u => u.Roles));
			this.CreateMap<UserRoleMapping, GetRolesForSessionDTO>()
				.ForMember(grfsd => grfsd.RoleName, x => x.MapFrom(urm => urm.Role.Name));
		}
	}
}
