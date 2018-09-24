using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace CodeFirst
{
    public class Repository<T>:IRepository<T> where T : class
    {
        private ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext _dbContext)
        {
            dbContext =_dbContext;
        }
        public IQueryable<T> Read()
        {
            return dbContext.Set<T>();
        }
        public void Insert(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Added;
        }
        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }

    }
}
