namespace Service.Converter.WebApi.Data.Context
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Domain.Entities;

    public class ConverterContext : DbContext
    {
        public ConverterContext(DbContextOptions<ConverterContext> options) : base(options)
        {
            try
            {
                Database.Migrate();

                LoadCharts();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Chart> Charts { get; set; }
        public DbSet<Audit> Audits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().Property(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Username).HasMaxLength(20).HasColumnType("varchar(20)");
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(250).HasColumnType("varchar(250)");
            modelBuilder.Entity<User>().Property(x => x.Password).HasMaxLength(250).HasColumnType("varchar(250)");
            modelBuilder.Entity<User>().Ignore(x => x.ValidationResult);
            modelBuilder.Entity<User>().Ignore(x => x.IsValid);
        }

        public void LoadCharts()
        {
            List<Chart> chartList = new List<Chart>();

            chartList.Add(new Chart(){ Id = new Guid(), Unit = "MilesToKilometers", ConstantValue = 0.62137m });
            chartList.Add(new Chart() { Id = new Guid(), Unit = "FahrenheitToCelsius", ConstantValue = 32 });
            chartList.Add(new Chart() { Id = new Guid(), Unit = "PoundToKilograms", ConstantValue = 2.20462m });

            foreach (var itemChart in chartList.Where(student => !this.Charts.Any(x => x.Unit == student.Unit)))
            {
                this.Charts.Add(entity: itemChart);

                this.SaveChanges();
            }

            this.SaveChanges();
        }
    }
}
