using Microsoft.EntityFrameworkCore;
using PetShelterWebAPI.Models;
using PetShelterWebAPI.Models.JoinTables;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PetShelterWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Specie> Species { get; set; }
        public DbSet<Breed> Breeds { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PetOwner>()
                .HasKey(po => new { po.PetId, po.OwnerId });
            modelBuilder.Entity<PetOwner>()
                .HasOne(p => p.Pet)
                .WithMany(po => po.PetOwners)
                .HasForeignKey(p => p.PetId);
            modelBuilder.Entity<PetOwner>()
               .HasOne(o => o.Owner)
               .WithMany(po => po.PetOwners)
               .HasForeignKey(o => o.OwnerId);
        }
    }
}