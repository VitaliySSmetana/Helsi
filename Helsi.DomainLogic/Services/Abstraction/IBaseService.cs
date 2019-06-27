using System;
using System.Collections.Generic;
using System.Linq;

namespace Helsi.DomainLogic.Services
{
    public interface IBaseService<T> where T : class
    {
        T Get(int id);
        T Create(T newItem, bool shouldBeCommited = true);
        IQueryable<T> GetAll();
        void Remove(int id, bool shouldBeCommited = true);
        void Commit(bool saveChanges = true);
    }
}
