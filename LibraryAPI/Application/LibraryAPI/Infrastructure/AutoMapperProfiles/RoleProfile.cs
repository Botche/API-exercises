namespace LibraryAPI.Infrastructure.AutoMapperProfiles
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

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
