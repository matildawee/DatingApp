using DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class PostRepository : Repository<Post>
    {
        public PostRepository(DatingAppContext context) : base(context) { }

        public List<Post> GetAllPostsByPersonId(int id)
        {
            return items.Where((p) => p.PersonId.Equals(id)).OrderByDescending((p) => p.Timestamp).ToList();
        }
    }
}
