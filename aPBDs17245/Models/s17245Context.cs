using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace aPBDs17245.Models
{
    public partial class s17245Context : DbContext
    {
        public s17245Context()
        {
        }

        public s17245Context(DbContextOptions<s17245Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artist { get; set; }
        public virtual DbSet<ArtistEvent> ArtistEvent { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventOrganiser> EventOrganiser { get; set; }

        public virtual DbSet<Organiser> Organiser { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s17245;Integrated Security=True;User ID=s17245;Password=x");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.HasKey(e => e.IdArtist)
                    .HasName("Artist_pk");

                entity.Property(e => e.IdArtist).ValueGeneratedNever();

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<ArtistEvent>(entity =>
            {
                entity.HasKey(e => new { e.IdEvent, e.IdArtist })
                    .HasName("Artist_Event_pk");

                entity.ToTable("Artist_Event");

                entity.Property(e => e.PerformanceDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdArtistNavigation)
                    .WithMany(p => p.ArtistEvent)
                    .HasForeignKey(d => d.IdArtist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Artist_Event_Event");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.ArtistEvent)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Artist_Event_Artist");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.IdEvent)
                    .HasName("Event_pk");

                entity.Property(e => e.IdEvent).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<EventOrganiser>(entity =>
            {
                entity.HasKey(e => new { e.IdEvent, e.IdOrganiser })
                    .HasName("Event_Organiser_pk");

                entity.ToTable("Event_Organiser");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany(p => p.EventOrganiser)
                    .HasForeignKey(d => d.IdEvent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Event_Organiser_Event");

                entity.HasOne(d => d.IdOrganiserNavigation)
                    .WithMany(p => p.EventOrganiser)
                    .HasForeignKey(d => d.IdOrganiser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Event_Organiser_Organiser");
            });

            modelBuilder.Entity<Organiser>(entity =>
            {
                entity.HasKey(e => e.IdOrganiser)
                    .HasName("Organiser_pk");

                entity.Property(e => e.IdOrganiser).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
