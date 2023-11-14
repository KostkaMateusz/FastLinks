using FastLinks.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace FastLinks.Persistence.Configurations;

public class UrlLinkConfiguration : IEntityTypeConfiguration<UrlLink>
{
    public void Configure(EntityTypeBuilder<UrlLink> builder)
    {
        builder.Property(t => t.UrlAddress).HasMaxLength(200).IsRequired();
    }
}