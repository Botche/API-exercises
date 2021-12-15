namespace LibraryAPI.Common.Constants
{
	public static class ExceptionMessages
	{
		public const string SOMETHING_WENT_WRONG_MESSAGE = "Something went wrong!";

		public const string ACTION_CONTEXT_ACCESSOR_IS_NULL_EXCEPTION = "The object \"actionContextAccessor\" is null";

		public const string BOOK_DOES_NOT_EXIST_MESSAGE = "Book with such an id does not exist!";

		public const string GENRE_BOOK_MAPPING_DOES_NOT_EXIST_MESSAGE = "There is no relation between this genre and book!";

		public const string GENRE_DOES_NOT_EXIST_MESSAGE = "Genre with such an id does not exist! ({0})";

		public const string GENRE_ALREADY_ADDED_MESSAGE = "Genre is already added to the book! ({0})";

		public const string ROLE_DOES_NOT_EXIST_MESSAGE = "Role with such a name does not exist!";

		public const string BOOK_USER_MAPPING_DOES_NOT_EXIST_MESSAGE = "There is no relation between this book and user!";

		public const string USER_DOES_NOT_EXIST_MESSAGE = "User with such an id does not exist!";

		public const string USER_EXIST_MESSAGE = "User with such an email already exists!";

		public const string PASSWORDS_MUST_MATCH_MESSAGE = "Passwords must match!";

		public const string USER_UNAUTHORIZED_MESSAGE = "User is unathorized for this kind of action!";

		public const string USER_UNAUTHENTICATED_MESSAGE = "Log in before continue!";
	}
}
