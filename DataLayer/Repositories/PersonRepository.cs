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
            return items.Where((p) => p.FirstName.Contains(name) && p.AccountHidden.Equals(false) || p.LastName.Contains(name) && p.AccountHidden.Equals(false)).ToList();
        }

        public int GetIdByUserIdentityEmail(string email)
        {
            int id = items.FirstOrDefault(p => p.Email.Equals(email)).PersonId;
            return id;
        }

        public Person GetPersonById(int id)
        {
            Person person = items.FirstOrDefault(p => p.PersonId == id);
            return person;
        }
        public byte[] GetPictureById(int id)
        {
            return items.FirstOrDefault(p => p.PersonId.Equals(id)).Picture;
        }
    }
}
