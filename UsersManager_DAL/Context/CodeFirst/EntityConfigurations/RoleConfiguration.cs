using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsersManager_DAL.Context.CodeFirst.InitialData;
using UsersManager_DAL.Domain;

namespace UsersManager_DAL.Context.CodeFirst.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasMany(r => r.UserRole)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(
                RoleData.User,
                RoleData.Admin,
                RoleData.Support,
                RoleData.SuperAdmin);
        }
    }
}