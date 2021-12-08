namespace LibraryAPI.Infrastructure.AutoMapperProfiles
{
	using AutoMapper;

	using LibraryAPI.Database.Models.Users;
	using LibraryAPI.DTOs.Role;

	public class RoleProfile : Profile
	{
		public RoleProfile()
		{
			this.CreateMap<Role, GetRoleIdDTO>();
		}
	}
}
