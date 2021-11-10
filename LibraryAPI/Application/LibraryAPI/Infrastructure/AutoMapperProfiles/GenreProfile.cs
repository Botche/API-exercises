namespace LibraryAPI.Infrastructure.AutoMapperProfiles
{
	using System.Collections.Generic;
	using System.Linq;

	using AutoMapper;

	using LibraryAPI.DTOs.Genre;
	using LibraryAPI.Database.Models.Books;

	public class GenreProfile : Profile
	{
		public GenreProfile()
		{
			this.CreateMap<PostGenreDTO, Genre>();
			this.CreateMap<Genre, GetGenreDTO>();
			this.CreateMap<IEnumerable<Genre>, GetAllGenreDTO>()
				.ForMember(gagbm => gagbm.Genres, g => g.MapFrom(genres => genres))
				.ForMember(gabbm => gabbm.GenresCount, g => g.MapFrom(genres => genres.Count()));
			this.CreateMap<PutGenreDTO, Genre>();
		}
	}
}
