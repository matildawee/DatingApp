using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class PersonRepository : Repository<Person>
    {
        public PersonRepository(DatingAppContext context) : base(context) { }

        public List<Person> GetAllVisiblePersons()
        {
            return items.Where((p) => p.AccountHidden.Equals(false)).ToList();
        }
        
        public List<Person> GetAllVisibleProfilesExceptCurrent(string mail)
        {
            return items.Where((p) => !p.Email.Equals(mail) && p.AccountHidden.Equals(false)).ToList();
        }

        public List<Person> SearchResultByName(string name, string email)
        {
            return items.Where((p) => p.FirstName.Contains(name) && p.AccountHidden.Equals(false) && !p.Email.Equals(email)
            || p.LastName.Contains(name) && p.AccountHidden.Equals(false) && !p.Email.Equals(email)).ToList();
        }

        public int GetIdByUserIdentityEmail(string email)
        {
            return items.FirstOrDefault(p => p.Email.Equals(email)).PersonId;
        }

        public Person GetPersonById(int id)
        {
            return items.FirstOrDefault(p => p.PersonId == id);
        }
        public byte[] GetPictureById(int id)
        {
            return items.FirstOrDefault(p => p.PersonId.Equals(id)).Picture;
        }
    }
}
