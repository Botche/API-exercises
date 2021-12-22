namespace LibraryAPI.Infrastructure.AutoMapperProfiles
{
	using System;

	using AutoMapper;

	using LibraryAPI.Common.Constants.ModelConstants;
	using LibraryAPI.Database.Models.Books;
	using LibraryAPI.DTOs.BookUserMapping;

	public class BookUserProfile : Profile
	{
		public BookUserProfile()
		{
			this.CreateMap<BookUserMapping, GetBookUserDTO>()
				.ForMember(
					gbu => gbu.PriceToPayForReturningAfterDeadLine,
					m => m.MapFrom(bum =>
						DateTime.Compare(DateTime.UtcNow, bum.DeadLine) > 0
							? Calculate(bum.DeadLine)
							: 0
					)
				);
		}

		private static double Calculate(DateTime deadLine)
		{
			var dateDifference = DateTime.UtcNow - deadLine;
			var totalDaysDifferenec = Math.Floor(dateDifference.TotalDays);

			var result = totalDaysDifferenec * BookUserMappingConstants.PRICE_PER_DAY_FOR_RETURNING_BOOK_AFTER_DEADLINE;

			var roundedResult = Math.Round(result, 2);
			return roundedResult;
		} 
	}
}
