namespace LibraryAPI.Common.Constants
{
	public static class ExceptionMessages
	{
		public const string SOMETHING_WENT_WRONG_MESSAGE = "Something went wrong!";

		public const string ACTION_CONTEXT_ACCESSOR_IS_NULL_EXCEPTION = "The object \"actionContextAccessor\" is null";

		public const string BOOK_DOES_NOT_EXIST_MESSAGE = "Book with such an id does not exist!";

		public const string GENRE_DOES_NOT_EXIST_MESSAGE = "Genre with such an id does not exist! ({0})";

		public const string GENRE_ALREADY_ADDED_MESSAGE = "Genre is already added to the book! ({0})";
	}
}
