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
			this.CreateMap<ICollection<Genre>, GetAllGenreDTO>()
				.ForMember(gagbm => gagbm.Genres, g => g.MapFrom(genres => genres))
				.ForMember(gabbm => gabbm.GenresCount, g => g.MapFrom(genres => genres.Count));
			this.CreateMap<ICollection<BookGenreMapping>, GetAllGenreDTO>()
				.ForMember(gagbm => gagbm.Genres, g => g.MapFrom(genres => genres))
				.ForMember(gabbm => gabbm.GenresCount, g => g.MapFrom(genres => genres.Count));
			this.CreateMap<BookGenreMapping, GetGenreDTO>()
				.ForMember(ggd => ggd.Id, bgm => bgm.MapFrom(mapping => mapping.GenreId))
				.ForMember(ggd => ggd.Name, bgm => bgm.MapFrom(mappin => mappin.Genre.Name));
			this.CreateMap<PutGenreDTO, Genre>();
		}
	}
}
