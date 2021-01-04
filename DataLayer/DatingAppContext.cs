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


            //modelBuilder.Entity<Friend>()     <<----- denna funkar att köra men ger bara en primary key
            //   .HasOne(d => d.FirstPerson)
            //   .WithMany()
            //   .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Friend>()
            //   .HasOne(d => d.SecondPerson)
            //   .WithMany()
            //   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()       // <<------ denna blir rätt med nycklar men blir fel med delete
                //.HasMany(p => p.FirstPerson)              man kan köra denna och ändra manuellt i migrationen sålänge
                //.WithRequired()                           till onDelete: ReferentialAction.Restrict);
                .HasKey(c => new { c.FirstPersonId, c.SecondPersonId });

            //modelBuilder.Entity<Friend>()
            //    .OnDelete(DeleteBehavior.Restrict);
                //.HasConstraintName("FK_Friend");
                //.WillCascadeOnDelete(false);

            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();


            //modelBuilder.Entity<Post>()    <<------ tror inte denna behövs
            //   .HasOne(d => d.Author)
            //   .WithMany()
            //   .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Post>()
            //   .HasOne(d => d.Person)
            //   .WithMany()
            //   .OnDelete(DeleteBehavior.Restrict);

        }
    }

    

}
