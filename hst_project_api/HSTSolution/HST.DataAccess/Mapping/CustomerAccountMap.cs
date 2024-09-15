using HST.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HST.DataAccess.Mapping
{
    public class CustomerAccountMap : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.ToTable("CustomerAccounts");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CustomerAccountNumber)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.CustomerAccountCurrency)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(c => c.CustomerAccountBalance)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasMany(c => c.CustomerAccountProcesses)
                .WithOne(p => p.ReceiverCustomer)
                .HasForeignKey(p => p.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            builder.HasData(
                new CustomerAccount
                {
                    Id = 1,
                    CustomerAccountNumber = "ACC123456",
                    CustomerAccountCurrency = "USD",
                    CustomerAccountBalance = 1000.50m,
                    AppUserID = 1
                },
                new CustomerAccount
                {
                    Id = 2,
                    CustomerAccountNumber = "ACC654321",
                    CustomerAccountCurrency = "EUR",
                    CustomerAccountBalance = 1500.75m,
                    AppUserID = 2
                });
        }
    }
}
