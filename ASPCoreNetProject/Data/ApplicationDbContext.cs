using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASPCoreNetProject.Models;

namespace ASPCoreNetProject.Data
{ 
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ASPCoreProject;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rate>()
                .HasKey(x => new { x.UserID, x.ArtID });
            modelBuilder.Entity<Rate>()
                .HasOne(x => x.AppUser)
                .WithMany(m => m.Rates)
                .HasForeignKey(x => x.UserID);
            modelBuilder.Entity<Rate>()
                .HasOne(x => x.Art)
                .WithMany(e => e.Rates)
                .HasForeignKey(x => x.ArtID);
            modelBuilder.Entity<Favourite>()
                .HasKey(x => new { x.UserID, x.ArtID });
            modelBuilder.Entity<Favourite>()
                .HasOne(x => x.AppUser)
                .WithMany(m => m.Favourites)
                .HasForeignKey(x => x.UserID);
            modelBuilder.Entity<Favourite>()
                .HasOne(x => x.Art)
                .WithMany(e => e.Favourites)
                .HasForeignKey(x => x.ArtID);
        }
        public virtual DbSet<Art> Arts { get; set; }
        public virtual DbSet<ArtPhoto> ArtPhotos { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Favourite> Favourites { get; set; }
    }
}
