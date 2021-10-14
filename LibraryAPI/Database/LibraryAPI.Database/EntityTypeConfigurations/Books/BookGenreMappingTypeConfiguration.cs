namespace LibraryAPI.Database.EntityTypeConfigurations.Books
{
	using LibraryAPI.Database.Models.Books;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class BookGenreMappingTypeConfiguration : IEntityTypeConfiguration<BookGenreMapping>
	{
		public void Configure(EntityTypeBuilder<BookGenreMapping> builder)
		{
			builder
				.HasOne(bgm => bgm.Book)
				.WithMany(b => b.Genres)
				.HasForeignKey(bgm => bgm.BookId);

			builder
				.HasOne(bgm => bgm.Genre)
				.WithMany(g => g.Books)
				.HasForeignKey(bgm => bgm.GenreId);
		}
	}
}
