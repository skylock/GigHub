using GigHub.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHub.Persistence.EntityConfigurations
{
    public class GigConfiguration : IEntityTypeConfiguration<Gig>
    {
        public void Configure(EntityTypeBuilder<Gig> builder)
        {
            builder
                .Property(g => g.ArtistId)
                .IsRequired();

            builder
                .Property(g => g.Venue)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(g => g.Genre)
                .IsRequired();
        }
    }
}
