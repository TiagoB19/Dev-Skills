using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DevsAndSkill.Model
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Desenvolvedor> Desenvolvedor { get; set; }
        public virtual DbSet<Habilidade> Habilidade { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Database");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Desenvolvedor>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Nome).IsUnicode(false);

                entity.Property(e => e.Site).IsUnicode(false);
            });

            modelBuilder.Entity<Habilidade>(entity =>
            {
                entity.Property(e => e.Nome).IsUnicode(false);

                entity.HasOne(d => d.Dev)
                    .WithMany(p => p.Habilidades)
                    .HasForeignKey(d => d.DevId)
                    .HasConstraintName("FK__HABILIDAD__DEV_I__60A75C0F")
                    .OnDelete(DeleteBehavior.Cascade);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
