using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Overhaul.Application.AutoFac;
using Overhaul.Application.Contracts;
using Overhaul.Domain.Common;
using Overhaul.Infrastructuer.Extentions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Domain.Entities;

namespace Overhaul.Infrastructuer.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private readonly Assembly assembly;

        public ApplicationDbContext(DbContextOptions options, Assembly assembly = null)
                 : base(options)
        {
            //Debugger.Launch();
            this.assembly = Assembly.GetAssembly(typeof(IEntity));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ExecuteModelBuilderConfigureAction();
            modelBuilder.RegisterAllEntities(assembly: assembly);
            modelBuilder.RegisterIEntityTypeConfiguration(assembly: Assembly.GetExecutingAssembly());
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddPluralizingTableNameConvention();

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("RowVersion")
                        .HasColumnType("varbinary(MAX)")
                        .HasDefaultValueSql("((0))");
                }
            }
        }
    }
}
