namespace LibraryAPI.Services.Database
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.Common.Constants;
	using LibraryAPI.Common.Exceptions;
	using LibraryAPI.Database;
	using LibraryAPI.Database.Models.Users;
	using LibraryAPI.Services.Database.Interfaces;

	using Microsoft.EntityFrameworkCore;

	public class RoleService : BaseService<Role>, IRoleService
	{
		public RoleService(LibraryAPIDbContext dbContext, IMapper mapper) 
			: base(dbContext, mapper)
		{
		}

		public async Task<T> GetRoleByNameAsync<T>(string name)
		{
			Role role = await this.DbSet
				.SingleOrDefaultAsync(r => r.Name == name);

			if (role == null)
			{
				throw new EntityDoesNotExist(ExceptionMessages.ROLE_DOES_NOT_EXIST_MESSAGE);
			}

			T roleToReturn = this.Mapper.Map<T>(role);
			return roleToReturn;
		}
	}
}
