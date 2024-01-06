using DataAccessLayer.Abstract;
using EntityLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{

    public class EfGenericRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        public void Add(TEntity entity)
        {
            using (RecycleCoinDbContext context = new RecycleCoinDbContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void AddList(List<TEntity> entities)
        {
            using (RecycleCoinDbContext context = new RecycleCoinDbContext())
            {
                foreach (var entity in entities)
                {
                    var addedEntity = context.Entry(entity);
                    addedEntity.State = EntityState.Added;
                }
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (RecycleCoinDbContext context = new RecycleCoinDbContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (RecycleCoinDbContext context = new RecycleCoinDbContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (RecycleCoinDbContext context = new RecycleCoinDbContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (RecycleCoinDbContext context = new RecycleCoinDbContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
