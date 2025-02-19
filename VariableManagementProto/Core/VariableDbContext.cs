﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class VariableDbContext : DbContext
    {
        string _connString;
        public VariableDbContext() {  }

        public VariableDbContext(DbContextOptions<VariableDbContext> options, string connString) : base(options) 
        {
            _connString = connString ?? throw new ArgumentNullException(nameof(connString));
        }
        public DbSet<Variable> Variables { get; set; }
        public DbSet<Models.TypeDefinition> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.TypeDefinition>().HasIndex(t => t.Name).IsUnique();

            modelBuilder.Entity<Variable>().HasOne(v => v.Type)
                                           .WithMany()
                                           .HasForeignKey(v => v.TypeId)
                                           .IsRequired()
                                           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Variable>()
            .HasIndex(v => v.Identifier)
            .IsUnique();
        }
    }
}
