using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.EntityFramework.Repository
{
    public abstract class GenericRepository<T> where T : class
    {
        protected Model _model;
        protected DbSet<T> _dbSet;

        public GenericRepository(Model model)
        {
            _model = model;
            _dbSet = _model.Set<T>();
        }

        public virtual EntityEntry<T> Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public void Edit(T entity)
        {
            _model.Entry(entity).State = EntityState.Modified;
        }

        public virtual EntityEntry<T> Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public virtual T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual bool Exists(int id)
        {
            return GetById(id) != null;
        }
    }
}
