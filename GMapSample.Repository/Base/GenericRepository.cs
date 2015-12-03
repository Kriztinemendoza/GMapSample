
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using GMapSample.DataModel;
using GMapSample.DataContract;

namespace GMapSample.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private GMapSampleContext _dataContext;
        private readonly IDbSet<T> _dbset;

        protected GenericRepository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected GMapSampleContext DataContext
        {
            get { return _dataContext ?? (_dataContext = DatabaseFactory.Get()); }
        }
        public virtual void AddOrUpdate(T entity)
        {
            _dbset.AddOrUpdate(entity);
        }
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }
        public virtual void Update(T entity)
        {
            _dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
        }
        public virtual T Get(int id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).ToList();
        }
        public T GetBy(Expression<Func<T, bool>> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }
    }
}
