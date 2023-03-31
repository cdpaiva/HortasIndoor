using HortasIndoor.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HortasIndoor.DataAccess.Data
{
    public class HIContext : IdentityDbContext<ApplicationUser>
    {
        public HIContext(DbContextOptions<HIContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // 1..n users and photos
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Photos)
                .WithOne(p => p.User);
            // 1..n users and posts
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User);
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User);
            builder.Entity<Post>()
                .HasMany(p => p.Likes)
                .WithOne(l => l.Post);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Like { get; set; }
    }
}
