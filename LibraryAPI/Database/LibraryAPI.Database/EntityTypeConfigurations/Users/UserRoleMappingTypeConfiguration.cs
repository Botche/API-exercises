namespace LibraryAPI.Database.EntityTypeConfigurations.Users
{
	using System;

	using LibraryAPI.Database.Models.Users;

	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class UserRoleMappingTypeConfiguration : IEntityTypeConfiguration<UserRoleMapping>
	{
		public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
		{
			builder
				.HasIndex(nameof(UserRoleMapping.UserId), nameof(UserRoleMapping.RoleId))
				.IsUnique(true);

			builder
				.HasOne(urm => urm.User)
				.WithMany(u => u.Roles)
				.HasForeignKey(urm => urm.UserId);

			builder
				.HasOne(urm => urm.Role)
				.WithMany(r => r.Users)
				.HasForeignKey(urm => urm.RoleId);
		}
	}
}
