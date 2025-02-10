using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    internal class VariableDbContext : DbContext
    {
        public DbSet<Variable> Variables { get; set; }
        public DbSet<Models.TypeDefinition> Types { get; set; }
        public VariableDbContext(DbContextOptions<VariableDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.TypeDefinition>().HasIndex(t => t.Name).IsUnique();

            modelBuilder.Entity<Variable>().HasOne(v => v.Type)
                                           .WithMany()
                                           .HasForeignKey(v => v.TypeId)
                                           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
