namespace LibraryAPI.Services.Database
{
	using System.Threading.Tasks;

	using AutoMapper;

	using LibraryAPI.Database;
	using LibraryAPI.Database.Models;

	using Microsoft.EntityFrameworkCore;

	public abstract class BaseService<TEntity>
		where TEntity : BaseModel
	{
		protected BaseService(LibraryAPIDbContext dbContext, IMapper mapper)
		{
			this.Mapper = mapper;
			this.DbContext = dbContext;
			this.DbSet = dbContext.Set<TEntity>();
		}

		protected IMapper Mapper { get; }

		protected LibraryAPIDbContext DbContext { get; private set; }

		protected DbSet<TEntity> DbSet { get; private set; }
	}
}
