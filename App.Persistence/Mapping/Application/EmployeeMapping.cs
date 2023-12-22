using Domain.Entities.Application;
using Domain.Entities.Identity;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Mapping.Application
{
    public static class EmployeeMapping
    {
        public static void AddEmployeeMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        }
        public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
        {
            public void Configure(EntityTypeBuilder<Employee> builder)
            {
                builder.Property(a => a.Name).HasMaxLength(200).IsRequired();
                builder.Property(a => a.Status).IsRequired();
                builder.Property(a => a.Agency).HasMaxLength(50).IsRequired();
                builder.Property(a => a.Department).HasMaxLength(50).IsRequired();
                builder.Property(a => a.Year).IsRequired();
                builder.Property(a => a.Rating).IsRequired();
                builder.Property(a => a.Category).HasConversion(v=>v.ToString() ,v=>(Enum.Parse<EmployeeCategory>(v)).ToString()).IsRequired();

            }
        }
    }
}
