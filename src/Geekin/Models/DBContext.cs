using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Geekin.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().ToTable("BlogPost");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }

}
