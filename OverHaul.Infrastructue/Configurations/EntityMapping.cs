using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelFare.Domain.Common;
using WelFare.Domain.Entities;

namespace WelFare.Infrastructure.Configurations;

public class EntityMapping : IEntityTypeConfiguration<Entity>
{
    public void Configure(EntityTypeBuilder<Entity> builder)
    {
        builder.Property(r => r.RowVersion).HasDefaultValue(new byte[1]);

    }
}
