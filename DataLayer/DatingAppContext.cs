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
            modelBuilder.Entity<Friend>().ToTable("Friend");
            modelBuilder.Entity<FriendRequest>().ToTable("FriendRequest");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Person>().ToTable("Person");

            modelBuilder.Entity<FriendRequest>()
               .HasOne(d => d.Sender)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FriendRequest>()
               .HasOne(d => d.Recevier)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Friend>()
               .HasOne(d => d.FirstPerson)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Friend>()
               .HasOne(d => d.SecondPerson)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>()
               .HasOne(d => d.Author)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Post>()
               .HasOne(d => d.Person)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);

        }
    }

    

}
