using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShowCase.Data.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShowCase.Data.DbContexts
{
    public class DataDbContext : IdentityDbContext<IdentityUser>
    {
        public DataDbContext(DbContextOptions<DataDbContext> options)
            : base(options)
        { }

        #region DbSets

        public DbSet<Page> Pages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Feature> Features { get; set; }

        public DbSet<Settings> Settings { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });
            modelBuilder.Entity<Settings>().HasData(new Settings { Id = 1, LogoUrl = "", FooterContent = "" });

            modelBuilder.Entity<Page>().ToTable("Pages");
            modelBuilder.Entity<Page>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Page>().HasOne(i => i.Parent).WithMany(u => u.Children).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Page>().HasMany(u => u.Children).WithOne(i => i.Parent);

            modelBuilder.Entity<Project>().ToTable("Projects");
            modelBuilder.Entity<Project>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Project>().HasMany(u => u.Features).WithOne(i => i.Project);

            modelBuilder.Entity<Feature>().ToTable("Features");
            modelBuilder.Entity<Feature>().Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Feature>().HasOne(i => i.Project).WithMany(u => u.Features).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Feature>().HasOne(i => i.Parent).WithMany(u => u.Children).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Feature>().HasMany(u => u.Children).WithOne(i => i.Parent);

            modelBuilder.Entity<Settings>().ToTable("Settings");
            modelBuilder.Entity<Settings>().Property(i => i.Id).ValueGeneratedOnAdd();
        }        
    }
}
