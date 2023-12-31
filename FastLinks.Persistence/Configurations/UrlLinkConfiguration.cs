﻿using FastLinks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastLinks.Persistence.Configurations;

public class UrlLinkConfiguration : IEntityTypeConfiguration<UrlLink>
{
    public void Configure(EntityTypeBuilder<UrlLink> builder)
    {
        builder.HasKey(ul => ul.ShortUrlAddress);
        builder.Property(t => t.UrlAddress).HasMaxLength(200).IsRequired();
    }
}