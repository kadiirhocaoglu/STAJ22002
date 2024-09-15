using HST.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HST.DataAccess.Mapping
{
    public class CustomerAccountProcessMap : IEntityTypeConfiguration<CustomerAccountProcess>
    {
        public void Configure(EntityTypeBuilder<CustomerAccountProcess> builder)
        {
            builder.ToTable("CustomerAccountProcesses");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProcessDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(p => p.PayerEmail)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.PayerPhoneNumber)
                .HasMaxLength(20);

            builder.Property(p => p.ProcessStatusEnum)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.ProcessTypeEnum)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            // Define the relationship and seed data
            builder.HasOne(p => p.ReceiverCustomer)
                .WithMany(c => c.CustomerAccountProcesses)
                .HasForeignKey(p => p.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new CustomerAccountProcess
                {
                    Id = 1,
                    ProcessDate = DateTime.Now,
                    PayerEmail = "payer1@example.com",
                    PayerPhoneNumber = "+1234567890",
                    ProcessStatusEnum = ProcessStatusEnum.Approved,
                    ProcessTypeEnum = ProcessTypeEnum.LinkPay,
                    Amount = 100.00m,
                    ReceiverID = 1,
                    Description = "Payment for invoice #001"
                },
                new CustomerAccountProcess
                {
                    Id = 2,
                    ProcessDate = DateTime.Now.AddDays(-1),
                    PayerEmail = "payer2@example.com",
                    PayerPhoneNumber = "+0987654321",
                    ProcessStatusEnum = ProcessStatusEnum.Pending,
                    ProcessTypeEnum = ProcessTypeEnum.CardPay,
                    Amount = 50.00m,
                    ReceiverID = 2,
                    Description = "Refund for order #123"
                });
        }
    }
}
