using HST.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mapping
{
    public class RoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            //Seeds
            builder.HasData(new AppRole
            {
                Id = 1,
                Name = "Superadmin",
                NormalizedName = "SUPERADMIN",
                ConcurrencyStamp = 1.ToString()
            },

        new AppRole
        {
            Id = 2,
            Name = "User",
            NormalizedName = "USER",
            ConcurrencyStamp = 2.ToString()
        },
          new AppRole
        {
            Id = 3,
            Name = "OperationSupport",
            NormalizedName = "OPERATIONSUPPORT",
            ConcurrencyStamp = 3.ToString()
        }); 
            

        }
    }
}
