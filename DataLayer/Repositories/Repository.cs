using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    //public interface IIdentifiable<T>
    //{
    //    T Id { get; set; }
    //}

    public abstract class Repository<TValue>  where TValue : class
    {
        private readonly DatingAppContext context;

        public Repository(DatingAppContext context)
        {
            this.context = context;
        }

        public DbSet<TValue> items => context.Set<TValue>();

        public void SaveChanges()
        {
            context.SaveChanges();
        }
        
    }
}
