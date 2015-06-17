using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Claudia.Domain.Models;
using Claudia.Domain.Models.v1;

namespace Claudia.Repository
{
    public interface IRepository<TModel> where TModel : class, IEntity
    {
        TModel Get(int id);
        IEnumerable<TModel> GetAll(bool showDeleted);

        //todo get all by page
        //IEnumerable<TModel> GetAllByPage(bool showDeleted, int take, int skip);
        IQueryable<TModel> Find(Expression<Func<TModel, bool>> predicate);
        void Add(TModel newEntity);
        void Remove(TModel entity);
        //void SaveChanges();
    }
}
