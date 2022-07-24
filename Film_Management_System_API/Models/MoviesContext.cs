using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Film_Management_System_API.Models
{
    public partial class MoviesContext : DbContext
    {
        public MoviesContext()
        {
        }

        public MoviesContext(DbContextOptions<MoviesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; } = null!;
        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Language> Languages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=FAST-ASUS\\SQLExpress;Database=Movies;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.ToTable("ACTOR");

                entity.Property(e => e.ActorId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Actor_id");

                entity.Property(e => e.FirstName)
                    .HasColumnType("text")
                    .HasColumnName("First_Name");

                entity.Property(e => e.LastName)
                    .HasColumnType("text")
                    .HasColumnName("Last_Name");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.AdminPasswordHash).HasMaxLength(100);

                entity.Property(e => e.AdminPasswordSalt).HasMaxLength(100);

                entity.Property(e => e.AdminUsernameEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Category_id");

                entity.Property(e => e.Name).HasColumnType("text");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("FILMS");

                entity.Property(e => e.FilmId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Film_id");

                entity.Property(e => e.ActorId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Actor_id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Category_id");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Language_id");

                entity.Property(e => e.Length).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.OriginalLanguageId)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Original_language_id");

                entity.Property(e => e.Rating).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.ReleaseYear)
                    .HasColumnType("text")
                    .HasColumnName("Release_Year");

                entity.Property(e => e.RentalDuration)
                    .HasColumnType("text")
                    .HasColumnName("Rental_Duration");

                entity.Property(e => e.ReplacementCost)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Replacement_cost");

                entity.Property(e => e.SpecialFeatures)
                    .HasColumnType("text")
                    .HasColumnName("Special_Features");

                entity.Property(e => e.Title).HasColumnType("text");

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.ActorId)
                    .HasConstraintName("FK_FILMS_ACTOR");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_FILMS_CATEGORY");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.FilmLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_FILMS_LANGUAGE");

                entity.HasOne(d => d.OriginalLanguage)
                    .WithMany(p => p.FilmOriginalLanguages)
                    .HasForeignKey(d => d.OriginalLanguageId)
                    .HasConstraintName("FK_FILMS_LANGUAGE1");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("LANGUAGE");

                entity.Property(e => e.LanguageId)
                    .HasColumnType("numeric(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Language_id");

                entity.Property(e => e.Name).HasColumnType("text");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
