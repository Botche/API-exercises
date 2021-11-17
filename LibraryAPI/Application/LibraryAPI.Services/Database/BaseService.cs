namespace LibraryAPI.Services.Database
{
	using System;
	using AutoMapper;

	using LibraryAPI.Common.Constants;
	using LibraryAPI.Database;
	using LibraryAPI.Database.Models;

	using Microsoft.AspNetCore.Mvc.Infrastructure;
	using Microsoft.EntityFrameworkCore;

	public abstract class BaseService<TEntity>
		where TEntity : BaseModel
	{
		private readonly IActionContextAccessor actionContextAccessor;

		protected BaseService(LibraryAPIDbContext dbContext,IMapper mapper)
		{
			actionContextAccessor = null;

			this.Mapper = mapper;
			this.DbContext = dbContext;
			this.DbSet = dbContext.Set<TEntity>();
		}

		protected BaseService(LibraryAPIDbContext dbContext, 
			IMapper mapper,
			IActionContextAccessor actionContextAccessor)
			: this(dbContext, mapper)
		{
			this.actionContextAccessor = actionContextAccessor;
		}

		protected IMapper Mapper { get; }

		protected LibraryAPIDbContext DbContext { get; private set; }

		protected DbSet<TEntity> DbSet { get; private set; }

		protected void AddModelError(string errorKey, string errorMessage)
		{
			if (this.actionContextAccessor == null)
			{
				throw new NullReferenceException(ExceptionMessages.ACTION_CONTEXT_ACCESSOR_IS_NULL_EXCEPTION);
			}

			this.actionContextAccessor.ActionContext.ModelState.AddModelError(errorKey, errorMessage);
		}
	}
}
