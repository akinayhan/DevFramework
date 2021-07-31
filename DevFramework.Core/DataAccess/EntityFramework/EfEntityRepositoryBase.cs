using DevFramework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContex> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContex : DbContext, new()
    {
        TEntity IEntityRepository<TEntity>.Add(TEntity entity)
        {
            using (var context = new TContex())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
                return entity;
            }
        }

        void IEntityRepository<TEntity>.Delete(TEntity entity)
        {
            using (var context = new TContex())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
  
            }
        }

        TEntity IEntityRepository<TEntity>.Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContex())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        List<TEntity> IEntityRepository<TEntity>.GetList(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContex())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        TEntity IEntityRepository<TEntity>.Update(TEntity entity)
        {
            using (var context = new TContex())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }
    }
}
