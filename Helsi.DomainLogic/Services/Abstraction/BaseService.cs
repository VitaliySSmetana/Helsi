using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helsi.DomainLogic.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly DbContext _context;

        public void Commit(bool saveChanges = true)
        {
            if (saveChanges)
                _context.SaveChanges();
        }

        public T Create(T newItem, bool shouldBeCommited = true)
        {
            _context.Set<T>().Add(newItem);
            Commit(shouldBeCommited);

            return newItem;
        }

        public T Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Remove(int id, bool shouldBeCommited = true)
        {
            throw new NotImplementedException();
        }
    }
}
