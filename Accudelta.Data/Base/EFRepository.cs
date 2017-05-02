using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using Accudelta.Data.Interface;

namespace Accudelta.Data.Base
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private DbContext _context;
        private DbSet<T> _objectSet;

        protected DbContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = GetCurrentUnitOfWork<EFUnitOfWork>().Context;
                }

                return _context;
            }
        }

        protected DbSet<T> ObjectSet
        {
            get
            {
                if (_objectSet == null)
                {
                    _objectSet = this.Context.Set<T>();
                }

                return _objectSet;
            }
        }

        public TUnitOfWork GetCurrentUnitOfWork<TUnitOfWork>() where TUnitOfWork : IUnitOfWork
        {
            return (TUnitOfWork)UnitOfWork.Current;
        }

        public IQueryable<T> GetQuery()
        {
            return ObjectSet;
        }

        public IEnumerable<T> GetAll()
        {
            return GetQuery().ToList();
        }

        public int Counter()
        {
            return GetQuery().Count();
        }

        public IEnumerable<T> Take(int total)
        {
            return this.ObjectSet.Take<T>(total);
        }
        public IEnumerable<T> PaginatedList(Expression<Func<T, int>> sort, int skipeRows, int pageSize)
        {
            return this.ObjectSet.OrderBy(sort).Skip<T>(skipeRows).Take(pageSize);
        }

        public IEnumerable<T> StoredProcedure(string name, object[] parameteres)
        {
            return this.ObjectSet.SqlQuery(name, parameteres);
        }


        public IEnumerable<T> Find(Func<T, bool> where)
        {
            return this.ObjectSet.Where<T>(where);
        }

        public T Single(Func<T, bool> where)
        {
            return this.ObjectSet.SingleOrDefault<T>(where);
        }

        public T First(Func<T, bool> where)
        {
            return this.ObjectSet.First<T>(where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this.ObjectSet.FirstOrDefault<T>(predicate);
        }

        public virtual void Delete(T entity)
        {
            this.ObjectSet.Remove(entity);
        }

        public virtual void Add(T entity)
        {
            this.ObjectSet.Add(entity);
        }

        public void Attach(T entity)
        {
            this.ObjectSet.Attach(entity);
        }

        public void SaveChanges()
        {
            this.Context.SaveChanges();
        }
    }
}
