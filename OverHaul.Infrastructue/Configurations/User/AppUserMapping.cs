using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Domain.Entities;

namespace Overhaul.Infrastructure.Configurations;

public class AppUserMapping : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasIndex(x => x.UserName).IsUnique(true);
        builder.HasIndex(x => x.NormalizedUserName).IsUnique(true);
    }
}
