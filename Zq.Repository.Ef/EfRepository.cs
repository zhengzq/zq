using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Zq.Common;
using Zq.DI;
using Zq.Domain;
using Zq.Logging;

namespace Zq.Repository.Ef
{

    public class EfRepository<T> : IRepository<T>
         where T : class, IAggregateRoot, new()
    {
        protected readonly EfDbContext Context;
        private IDbSet<T> _entities;
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = Context.Set<T>();
                return _entities;
            }
        }
        public EfRepository(EfDbContext context)
        {
            this.Context = context; 
        }
        public virtual T Get(object id)
        {
            return this.Entities.Find(id);
        }
        public virtual void Add(T aggregateRoot)
        {
            try
            {
                Ensure.IsNotNull(aggregateRoot, "aggregateRoot");

                this.Entities.Add(aggregateRoot);
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                var logger = DI.Ioc.Resolve<ILogger>();
                logger.Log(LogLevel.Error, fail.Message, fail);
                throw fail;
            }
        }
        public virtual void Update(T aggregateRoot)
        {
            try
            {
                Ensure.IsNotNull(aggregateRoot, "aggregateRoot");
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                var logger = DI.Ioc.Resolve<ILogger>();
                logger.Log(LogLevel.Error, fail.Message, fail);
                throw fail;
            }
        }
        public virtual void Delete(T aggregateRoot)
        {
            try
            {
                Ensure.IsNotNull(aggregateRoot, "aggregateRoot");

                this.Entities.Remove(aggregateRoot);

            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                var logger = DI.Ioc.Resolve<ILogger>();
                logger.Log(LogLevel.Error, fail.Message, fail);
                throw fail;
            }
        }
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }
    }
}
