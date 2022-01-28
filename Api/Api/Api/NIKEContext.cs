using System;
using Api.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Api
{
    public partial class NIKEContext : DbContext
    {
        public NIKEContext()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public NIKEContext(DbContextOptions<NIKEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Entry> Entries { get; set; }
        public virtual DbSet<POI> POI { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<LikeDislikeEntry> LikeDislikeEntry { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");
                entity.HasOne(e => e.Country).WithMany(p => p.Cities).HasForeignKey(o => o.CountryId).HasConstraintName("FK_Country");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comment1).HasColumnName("Comment");

                entity.Property(e => e.EntryId).HasColumnName("EntryID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Entry>(entity =>
            {
                entity.ToTable("Entry");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.POIID).HasColumnName("POIID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(x => x.User).WithMany(e => e.Entries).HasForeignKey(o => o.UserId).HasConstraintName("UserID");
                entity.HasOne(x => x.POI).WithMany(e => e.Entries).HasForeignKey(o => o.POIID).HasConstraintName("POIID");

            });

            modelBuilder.Entity<POI>(entity =>
            {
                entity.ToTable("POI");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Longitude).HasColumnName("Longitude");

                entity.Property(e => e.Latitude).HasColumnName("Latitude");
                entity.HasOne(e => e.City).WithMany(p => p.POI).HasForeignKey(o => o.CityID).HasConstraintName("FK_City");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.ToTable("Reaction");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EntryId).HasColumnName("EntryID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("TEXT(250)");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnType("TEXT(45)");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnType("TEXT(100)");

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Username).IsRequired();
            });

            modelBuilder.Entity<LikeDislikeEntry>(entity =>
            {
                entity.ToTable("LikeDislikeEntry");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EntryId)
                    .HasColumnName("EntryId");

                entity.Property(e => e.Likes)
                    .IsRequired()
                    .HasColumnName("Likes");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserId");

                entity.HasOne(e => e.User).WithMany(e => e.LikeDislikeEntries).HasForeignKey(o => o.UserId).HasConstraintName("UID");
                entity.HasOne(e => e.Entry).WithMany(e => e.LikeDislikeEntries).HasForeignKey(o => o.EntryId).HasConstraintName("EID");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
