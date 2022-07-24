using M2c.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace M2c.Infrastructure.EntityConfigurations
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", M2CDbContext.DefaultSchema);
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Firstname).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Lastname).HasMaxLength(100).IsRequired();
            builder.Property(b => b.PhoneNumber).HasMaxLength(15);
            builder.Property(b => b.Email).HasMaxLength(50);
            builder.HasIndex(b => b.Email).IsUnique();
            builder.HasIndex(p => new { p.Firstname, p.Lastname, p.DateOfBirth }).IsUnique();
        }
    }
}