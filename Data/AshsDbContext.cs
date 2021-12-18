using Microsoft.EntityFrameworkCore;
using PostgresConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgresConnect.Data
{
    public class AshsDbContext : DbContext
    {
        public AshsDbContext(DbContextOptions<AshsDbContext> options)
            : base(options)
        {

        }

        public DbSet<Ash> Ashs { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<ToDoTask> ToDoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ash>().ToTable("Ash");
            //Add stuff here
            modelBuilder.Entity<Pizza>().ToTable("Pizza");
            modelBuilder.Entity<ToDoTask>().ToTable("ToDoTask");
            base.OnModelCreating(modelBuilder);
        }
    }
}
