using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersManager_DAL.Domain;

namespace UsersManager_DAL.Context.CodeFirst.EntityConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(p => p.Value)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.IsUsed)
                .HasDefaultValue(false);

            builder.Property(p => p.CreatedDate)
                .HasDefaultValueSql("getdate()");

            builder.Property(p => p.ExpiredDate)
                .IsRequired();

            builder.HasOne(rt => rt.User)
                .WithMany()
                .HasForeignKey(rt => rt.UserId);
        }
    }
}