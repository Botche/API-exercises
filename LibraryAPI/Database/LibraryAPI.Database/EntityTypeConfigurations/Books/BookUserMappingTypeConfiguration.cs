namespace LibraryAPI.Database.EntityTypeConfigurations.Books
{
	using LibraryAPI.Database.Models.Books;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class BookUserMappingTypeConfiguration : IEntityTypeConfiguration<BookUserMapping>
	{
		public void Configure(EntityTypeBuilder<BookUserMapping> builder)
		{
			builder
				.HasIndex(nameof(BookUserMapping.BookId), nameof(BookUserMapping.UserId))
				.IsUnique(true);

			builder
				.HasOne(bum => bum.Book)
				.WithMany(b => b.Users)
				.HasForeignKey(bum => bum.BookId);

			builder
				.HasOne(bum => bum.User)
				.WithMany(u => u.Books)
				.HasForeignKey(bum => bum.UserId);
		}
	}
}
