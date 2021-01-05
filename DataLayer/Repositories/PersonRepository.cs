using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(DatingAppContext context) : base(context) { }

        public Person GetPersonById(int Id)
        {
            return items.Find(Id);
        }

        public List<Person> GetAllPersons()
        {
            return items.ToList();
        }
        
        public List<Person> GetAllProfilesExceptCurrent(string mail)
        {
            return items.Where((p) => !p.Email.Equals(mail)).ToList();
        }

        public List<Person> SearchResultByName(string name)
        {
            return items.Where((p) => p.FirstName.Contains(name) || p.LastName.Contains(name)).ToList();
        }
    }
}
