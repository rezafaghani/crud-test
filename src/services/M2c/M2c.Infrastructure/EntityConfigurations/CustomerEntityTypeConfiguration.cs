﻿using M2c.Domain.AggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace M2c.Infrastructure.EntityConfigurations
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", M2CDbContext.DefaultSchema);
            builder.HasKey(b => new { b.Firstname, b.Lastname, b.DateOfBirth });
            builder.Property(b => b.Firstname).IsRequired();
            builder.Property(b => b.Lastname).IsRequired();
            builder.Property(b => b.PhoneNumber).HasMaxLength(15);
        }
    }
}