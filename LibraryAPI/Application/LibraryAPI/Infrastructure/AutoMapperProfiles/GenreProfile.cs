namespace LibraryAPI.Infrastructure.AutoMapperProfiles
{
	using System.Collections.Generic;
	using System.Linq;

	using AutoMapper;

	using LibraryAPI.BindingModels.Genre;
	using LibraryAPI.Database.Models.Books;

	public class GenreProfile : Profile
	{
		public GenreProfile()
		{
			this.CreateMap<PostGenreBindingModel, Genre>();
			this.CreateMap<Genre, GetGenreBindingModel>();
			this.CreateMap<IEnumerable<Genre>, GetAllGenreBindingModel>()
				.ForMember(gagbm => gagbm.Genres, g => g.MapFrom(genres => genres))
				.ForMember(gabbm => gabbm.GenresCount, g => g.MapFrom(genres => genres.Count()));
			this.CreateMap<PutGenreBindingModel, Genre>();
		}
	}
}
