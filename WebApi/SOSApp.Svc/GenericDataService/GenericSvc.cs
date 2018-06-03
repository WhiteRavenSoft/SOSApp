using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using WhiteRaven.Svc.Infrastructure;

namespace WhiteRaven.Svc.GenericDataService
{
    public class GenericSvc<TEntity, TContext> : IGenericSvc<TEntity>, IDisposable
        where TEntity : class
        where TContext : DbContext
    {
        protected static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private TContext _context;


        protected IEnumerable<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters)
        {

            return this.InnerContext.ExecuteStoreQuery<TElement>(commandText, parameters).AsEnumerable();

        }

        private ObjectContext InnerContext
        {
            get { return ((IObjectContextAdapter)this.Context).ObjectContext; }
        }

        protected TContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = IoC.Resolve<TContext>();
                    _context.Configuration.ProxyCreationEnabled = false;
                    _context.Configuration.LazyLoadingEnabled = false;
                    _context.Configuration.ValidateOnSaveEnabled = false;
                    _context.Configuration.AutoDetectChangesEnabled = false;
                }
                return _context;
            }
        }

        public virtual TEntity Create()
        {
            return Context.Set<TEntity>().Create();
        }

        public virtual TEntity CreateEntity(TEntity entity)
        {
            TEntity newentity = Context.Set<TEntity>().Add(entity);

            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("{0}", validationError.ErrorMessage);
                    }
                }

                throw new Exception(msg);
            }

            return newentity;
        }

        public virtual TEntity UpdateEntity(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;

            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("{0}", validationError.ErrorMessage);
                    }
                }

                throw new Exception(msg);
            }

            return entity;
        }

        public virtual void Delete(int id)
        {
            var item = Load(id);
            Context.Set<TEntity>().Remove(item);

            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("{0}", validationError.ErrorMessage);
                    }
                }

                throw new Exception(msg);
            }
        }

        public virtual void DeleteByGuid(Guid id)
        {
            var item = LoadByGuid(id);
            Context.Set<TEntity>().Remove(item);
            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("{0}", validationError.ErrorMessage);
                    }
                }

                throw new Exception(msg);
            }
        }

        public virtual void DeleteEntity(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);

            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("{0}", validationError.ErrorMessage);
                    }
                }

                throw new Exception(msg);
            }
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            var objects = Context.Set<TEntity>().Where(where).AsEnumerable();

            foreach (var item in objects)
                Context.Set<TEntity>().Remove(item);

            try
            {
                this.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("{0}", validationError.ErrorMessage);
                    }
                }

                throw new Exception(msg);
            }
        }

        public virtual TEntity Load(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual TEntity LoadByGuid(Guid id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual bool HasAny(Expression<Func<TEntity, bool>> where)
        {
            return Context.Set<TEntity>().Any(where);
        }

        public virtual TEntity FindOne(Expression<Func<TEntity, bool>> where = null)
        {
            return LoadAll(where).AsEnumerable().FirstOrDefault();
        }

        public virtual TEntity FindLast(Expression<Func<TEntity, bool>> where = null)
        {
            return LoadAll(where).AsEnumerable().LastOrDefault();
        }

        public virtual TEntity FindSingle(Expression<Func<TEntity, bool>> where = null)
        {
            return LoadAll(where).SingleOrDefault();
        }

        public virtual TEntity FindFirst(Expression<Func<TEntity, bool>> where = null)
        {
            return Context.Set<TEntity>().FirstOrDefault(where);
        }

        public virtual async Task<TEntity> FindFirstAsync(Expression<Func<TEntity, bool>> where = null)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(where);
        }

        public IQueryable<T> Set<T>() where T : class
        {
            return Context.Set<T>();
        }

        public virtual IQueryable<TEntity> LoadAll(Expression<Func<TEntity, bool>> where = null)
        {
            IQueryable<TEntity> rdo = null != where ? Context.Set<TEntity>().Where(where) : Context.Set<TEntity>();
            return rdo;
        }

        public virtual bool LoadAny(Expression<Func<TEntity, bool>> where)
        {
            return Context.Set<TEntity>().Any(where);
        }

        public virtual IQueryable<TEntity> LoadPagedSkip(int skip, int take, out int total, Func<TEntity, object> sortname, bool desc, Expression<Func<TEntity, bool>> where = null)
        {
            if (sortname == null)
                throw new Exception("You must complete sortname parameter.");

            IEnumerable<TEntity> result = null;

            if (take > 0)
            {
                if (desc)
                    result = Context.Set<TEntity>().Where(where).OrderByDescending(sortname).Skip(skip).Take(take);
                else
                    result = Context.Set<TEntity>().Where(where).OrderBy(sortname).Skip(skip).Take(take);
            }
            else
            {
                if (desc)
                    result = Context.Set<TEntity>().Where(where).OrderByDescending(sortname);
                else
                    result = Context.Set<TEntity>().Where(where).OrderBy(sortname);
            }

            total = Context.Set<TEntity>().Where(where).Count();

            return result.AsQueryable();
        }

        public void SetCommandTimeout(int? timeOut)
        {
            this.InnerContext.CommandTimeout = timeOut;
        }

        public int? GetCommandTimeout()
        {
            return InnerContext.CommandTimeout;
        }

        protected virtual bool SaveChanges()
        {
            try
            {
                return 0 < Context.SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (null != Context)
                Context.Dispose();
        }

        public void LoadProperty(object entity, string navigationProperty)
        {
            this.InnerContext.LoadProperty(entity, navigationProperty);
        }

        protected bool StoredProcedureEnabled
        {
            get
            {
                var rdo = Convert.ToBoolean(ConfigurationManager.AppSettings["WhiteRaven.Svc.Configuration.StoredProcedure.Enabled"]);
                return rdo;
            }
        }

        public void ExecStoredProcedureManual(string storeName, List<object> parameters)
        {
            var tsqlPattern = "exec {0} {1}";

            var strParam = "";

            for (int i = 0; i < parameters.Count; i++)
                strParam += "@p" + i.ToString() + ",";

            strParam = strParam.TrimEnd(",".ToCharArray());

            var tsql = string.Format(tsqlPattern, storeName, strParam);


            Context.Database.ExecuteSqlCommand(tsql, parameters.ToArray());
        }

        public DbRawSqlQuery<T> ExecStoredProcedureManual<T>(string storeName, List<object> parameters)
                where T : class
        {
            var tsqlPattern = "exec {0} {1}";

            var strParam = "";

            for (int i = 0; i < parameters.Count; i++)
                strParam += "@p" + i.ToString() + ",";

            strParam = strParam.TrimEnd(",".ToCharArray());

            var tsql = string.Format(tsqlPattern, storeName, strParam);

            var query = Context.Database.SqlQuery<T>(tsql, parameters.ToArray());

            return query;
        }

        public virtual TEntity IdentityInsert(TEntity entity)
        {
            try
            {
                var tbl = entity.GetType().Name;

                //var idon = Context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[" + tbl + "] ON");

                TEntity newentity = Context.Set<TEntity>().Add(entity);

                this.SaveChanges();

                //var idoff = Context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[" + tbl + "] OFF");

                return newentity;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += string.Format("{0}", validationError.ErrorMessage);
                    }
                }

                throw new Exception(msg);
            }
        }
    }
}
