using System.Collections.Generic;
using System.Linq;
using WebApplication3.Models;

namespace CodeFirst
{
    public interface IRepository<T>
    {
        IQueryable<T> Read();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}