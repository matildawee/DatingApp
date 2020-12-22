using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class DatingAppContext : DbContext
    {
        public DatingAppContext (DbContextOptions<DatingAppContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>().ToTable("Friends");
            modelBuilder.Entity<FriendRequest>().ToTable("FriendRequests");
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Person>().ToTable("Users");

            modelBuilder.Entity<FriendRequest>()
               .HasOne(d => d.Sender)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Friend>()
               .HasOne(d => d.FirstPerson)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>()
               .HasOne(d => d.Author)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
                
        }
    }

    

}
