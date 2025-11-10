using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.User)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(t => t.Estatus)
               .HasMaxLength(20)
               .IsRequired();
    }
}
