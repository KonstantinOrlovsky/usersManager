using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersManager_DAL.Domain;

namespace UsersManager_DAL.Context.CodeFirst.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Id)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(70)
                .IsRequired();

            builder.Property(p => p.PasswordHash)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Age)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}