using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{

    public class SignsContext : DbContext
    {
        public DbSet<Sign> Signs { get; set; }
        public DbSet<User> Users { get; set; }
        public SignsContext(DbContextOptions<SignsContext> options)
            : base(options)
        {
            Database.EnsureCreated(); 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { entity.HasIndex(e => e.Email).IsUnique(); });
        }
    }
}
