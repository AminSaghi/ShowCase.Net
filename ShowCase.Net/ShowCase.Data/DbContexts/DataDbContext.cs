using Microsoft.EntityFrameworkCore;
using ShowCase.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShowCase.Data.DbContexts
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        { }

        #region DbSets

        public DbSet<Page> Pages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Feature> Features { get; set; }

        #endregion

        public static DataDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataDbContext>();
            
            return new DataDbContext(optionsBuilder.Options);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);       

            modelBuilder.Entity<Page>().ToTable("Pages");
            modelBuilder.Entity<Page>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Page>().HasMany(u => u.Children).WithOne(i => i.Parent);

            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<Project>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Project>().HasMany(u => u.Features).WithOne(i => i.Project);

            modelBuilder.Entity<Feature>().ToTable("Features");
            modelBuilder.Entity<Feature>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Feature>().HasOne(i => i.Project).WithMany(u => u.Features);
            modelBuilder.Entity<Feature>().HasOne(i => i.Parent).WithMany(u => u.Children);
            modelBuilder.Entity<Feature>().HasMany(u => u.Children).WithOne(i => i.Parent);
        }
    }
}
