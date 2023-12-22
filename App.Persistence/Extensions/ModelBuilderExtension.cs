using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Shared.Constants;

namespace Persistence.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void ConfigurTable(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                       .SelectMany(t => t.GetProperties())
                       .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType(EntityConfig.SqlDecimalType);
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
            {
                property.SetColumnType(EntityConfig.SqlDateType);
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                {
                    property.SetMaxLength(256);
                }
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(Guid) && p.Name == "Id"))
            {
                property.SetDefaultValueSql(EntityConfig.SqlGuidDefaultValue);
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.Name == "CreatedOn"))
            {
                property.SetDefaultValueSql(EntityConfig.SqlDateDefaultValue);
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.Name == "IsDeleted"))
            {
                property.SetDefaultValueSql(EntityConfig.SqlRowDeletedDefaultValue);
            }




            //modelBuilder.ApplyConfiguration(new DashboardConfig());
        }
    }
}
