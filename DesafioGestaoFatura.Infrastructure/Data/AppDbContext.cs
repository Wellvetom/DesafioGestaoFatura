using DesafioGestaoFatura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioGestaoFatura.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<ItemFatura> Itens { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemFatura>()
            .HasOne(i => i.Fatura)
            .WithMany(f => f.Itens)
            .HasForeignKey(i => i.FaturaId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Fatura>(entity =>
            {
                modelBuilder.Entity<ItemFatura>()
              .HasOne(i => i.Fatura)
              .WithMany(f => f.Itens)
              .HasForeignKey(i => i.FaturaId)
              .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
