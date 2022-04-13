using DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Repo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly product_testContext dbContext;

        public Repository(product_testContext _dbContext)
        {
            dbContext = _dbContext;
            this.Table = dbContext.Set<T>();
        }

        public DbSet<T> Table { get; set; }

        public bool Add(T entity)
        {
            Table.Add(entity);
            return Save();
        }

        public bool Update(T entity)
        {
            Table.Update(entity);
            return Save();
        }

        public bool Delete(T entity)
        {
            Table.Remove(entity);
            return Save();
        }

        public IQueryable<T> All()
        {
            return Table;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return Table.Where(where);
        }

        public IQueryable<T> OrderBy<TKey>(Expression<Func<T, TKey>> orderBy, bool isDesc)
        {
            if (isDesc)
                return Table.OrderByDescending(orderBy);
            return Table.OrderBy(orderBy);
        }


        private bool Save()
        {
            try
            {
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                // TODO: Log Exceptions
                return false;
            }
        }

        public T FindById(object id)
        {
            return Table.Find(id);
        }
    }
}
