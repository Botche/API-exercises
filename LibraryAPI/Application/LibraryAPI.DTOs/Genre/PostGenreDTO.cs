﻿namespace LibraryAPI.DTOs.Genre
{
	using System.ComponentModel.DataAnnotations;

	using LibraryAPI.DTOs.Constants;

	public class PostGenreDTO
	{
		[Required]
		[StringLength(GenreConstants.NAME_MAX_LENGTH)]
		public string Name { get; set; }
	}
}