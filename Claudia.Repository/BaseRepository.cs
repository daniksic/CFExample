using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v1;

namespace Claudia.Repository
{
    public abstract class BaseRepository<TModel> : IRepository<TModel> where TModel : class, IEntity
    {
        internal readonly DbSet<TModel> EntitySet;
        internal readonly DbContext Dbcontext;

        protected BaseRepository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Dbcontext = context;
            EntitySet = Dbcontext.Set<TModel>();
        }


        public TModel Get(int id)
        {
            return EntitySet.Find(id);
        }

        public virtual IEnumerable<TModel> GetAll(bool showDeleted = false)
        {
            return showDeleted ? EntitySet.ToList() : EntitySet.Where(s => s.IsDeleted != true).ToList();
        }

        public IQueryable<TModel> Find(Expression<Func<TModel, bool>> predicate)
        {
            return EntitySet.Where(predicate);
        }

        public virtual void Add(TModel newEntity)
        {
            EntitySet.Add(newEntity);
        }

        public virtual void Attach(TModel model)
        {
            Dbcontext.Entry(model).State = EntityState.Modified;
        }

        public virtual void Remove(TModel entity)
        {
            //EntitySet.Remove(entity);
            entity.IsDeleted = true;
        }

    }
}
