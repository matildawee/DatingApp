﻿using DataLayer;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(DatingAppContext context)
        {
            context.Database.EnsureCreated();

            //Look for any students.
            //if (context.Persons.Any())
            //    {
            //        return;   // DB has been seeded
            //    }

            //var persons = new Person[]
            //{
            //    new Person {Email = "matilda@hej.se", FirstName = "Matilda",    LastName = "Barzdukas",
            //        Description = "hej4" },
            //    new Person {Email = "oskar@hej.se", FirstName = "Oskar",    LastName = "Barzdukas",
            //        Description = "hej4" },
            //    new Person {Email = "ptheven@hej.se", FirstName = "Phteven",    LastName = "Barzdukas",
            //        Description = "hej4" }
            //};

            //foreach (Person p in persons)
            //{
            //    context.Persons.Add(p);
            //}
            //context.SaveChanges();

            //var posts = new Post[]
            //{
            //    new Post {PersonId = 1, AuthorId = 2,
            //        PostText = "hej", Timestamp = DateTime.Now}
            //};
            //foreach (Post p in posts)
            //{
            //    context.Posts.Add(p);
            //}
            //context.SaveChanges();
        }
    }
}
