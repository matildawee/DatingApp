using DataLayer;
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

            // Look for any students.
            if (context.Persons.Any())
            {
                return;   // DB has been seeded
            }

            var persons = new Person[]
            {
                new Person { FirstName = "Carson",   LastName = "Alexander",
                    Description = "hej" },
                new Person { FirstName = "Meredith", LastName = "Alonso",
                    Description = "hej2" },
                new Person { FirstName = "Arturo",   LastName = "Anand",
                    Description = "hej3" },
                new Person { FirstName = "Gytis",    LastName = "Barzdukas",
                    Description = "hej4" }
            };

            foreach (Person p in persons)
            {
                context.Persons.Add(p);
            }
            context.SaveChanges();
        }
    }
}
