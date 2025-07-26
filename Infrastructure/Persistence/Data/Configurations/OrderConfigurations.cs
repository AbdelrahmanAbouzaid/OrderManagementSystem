
using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.TotalAmount)
           .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Status)
                .HasConversion(
                    s => s.ToString(),
                    s => Enum.Parse<OrderStatus>(s));

            builder.Property(o => o.PaymentMethod)
                .HasConversion(
                    p => p.ToString(),
                    p => Enum.Parse<PaymentMethod>(p));

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Invoice)
                .WithOne(i => i.Order)
                .HasForeignKey<Invoice>(i => i.OrderId);
        }
    }
}
