using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GMapSample.DataContract
{
    public interface IGenericRepository<T> where T : class
    {
        void AddOrUpdate(T entity);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        T Get(int Id);
        T GetBy(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}
