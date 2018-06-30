using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SOSApp.Svc.GenericDataService
{
    public interface IGenericSvc<TEntity>
    {
        TEntity Create();

        TEntity CreateEntity(TEntity entity);

        TEntity UpdateEntity(TEntity entity);

        void Delete(int id);

        void DeleteByGuid(Guid id);

        void DeleteEntity(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> where);

        TEntity FindOne(Expression<Func<TEntity, bool>> where = null);

        TEntity FindSingle(Expression<Func<TEntity, bool>> where = null);

        TEntity Load(int id);

        TEntity LoadByGuid(Guid id);

        IQueryable<TEntity> LoadAll(Expression<Func<TEntity, bool>> where = null);

        IQueryable<T> Set<T>() where T : class;
    }
}
