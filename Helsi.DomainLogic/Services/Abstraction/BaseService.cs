using Helsi.Core.Entities;
using Helsi.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Helsi.DomainLogic.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, IEntity<int>
    {
        protected readonly HelsiContext _context;

        public BaseService(HelsiContext context)
        {
            _context = context;
        }

        public void Commit(bool saveChanges = true)
        {
            if (saveChanges)
                _context.SaveChanges();
        }

        public TEntity Create(TEntity newItem, bool shouldBeCommited = true)
        {
            _context.Set<TEntity>().Add(newItem);
            Commit(shouldBeCommited);

            return newItem;
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().FirstOrDefault(entity => entity.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        public void Remove(int id, bool shouldBeCommited = true)
        {
            throw new NotImplementedException();
        }
    }
}
