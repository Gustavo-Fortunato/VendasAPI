﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VendasAPI.Domain.Entities;

namespace VendasAPI.Data.Configuration
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItemVenda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de chave composta
            modelBuilder.Entity<ItemVenda>().HasKey(iv => new { iv.ProdutoId, iv.VendaId });
        }
    }
}