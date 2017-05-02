using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Accudelta.Data.Interface
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> where);
        IEnumerable<T> Take(int total);
        IEnumerable<T> StoredProcedure(string name, object[] parameteres);
        IEnumerable<T> PaginatedList(Expression<Func<T, int>> sort, int skipeRows, int pageSize);
        T Single(Func<T, bool> where);
        T First(Func<T, bool> where);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        int Counter();
        void Delete(T entity);
        void Add(T entity);
        void Attach(T entity);
        void SaveChanges();
    }
}
