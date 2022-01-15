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
        public DbSet<Question> Question { get; set; }
        public DbSet<Board> Board { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().ToTable("question");
            modelBuilder.Entity<Board>().ToTable("board");
            base.OnModelCreating(modelBuilder);
        }
    }
}
