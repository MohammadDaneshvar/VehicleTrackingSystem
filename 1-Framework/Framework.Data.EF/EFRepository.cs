using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Framework.Domain;
using Framework.Domain.Aggregate;
using Framework.Domain.Events;
using Framework.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Framework.Data.EF
{

  

    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class /*,IAggregateRoot*/
    {
        private readonly IDbContext _dbContext;
        private IEventDispatcher eventDispatcher;
        private MethodInfo dispatchMethod;
        public EFRepository(IDbContext dbContext, IEventDispatcher eventDispatcher)
        {
            this._dbContext = dbContext;
            this.eventDispatcher = eventDispatcher;
            dispatchMethod = eventDispatcher.GetType().GetMethod("Dispatch");
        }

        public async Task AddAsync(TEntity entity)
        {
            await this._dbContext.AddAsync(entity);
       //     DispatchEvents(entity);
        }
        readonly JsonSerializerSettings setting = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        //private void DispatchEvents(TEntity entity)
        //{
        //    var events = entity.GetUnPublishedEvents().ToList();
        //    entity.ClearEvents();
        //    events.ForEach(@event =>
        //    {
        //        @event.AggregateRootId = entity.Id.ToString();
        //        @event.CreateDate = DateTime.Now;
        //        @event.UserName = Thread.CurrentPrincipal.Identity.Name;
        //        dispatchMethod.MakeGenericMethod(@event.GetType()).Invoke(eventDispatcher, new object[] { @event });
        //    });
        //    foreach (var e in events)
        //    {
        //        //session.Save(new Event(e.GetType().FullName, e.UserName, e.AggregateRootId,
        //        //    JsonConvert.SerializeObject(e, setting), e.GetType().FullName));
        //    }
        //}

        public async Task AddRangeAsync(IEnumerable<TEntity> items)
        {
            await _dbContext.AddRangeAsync(items);
        }

        public bool Update(TEntity entity)
        {
            _dbContext.Update(entity);
          //  DispatchEvents(entity);

            return true;
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Remove(entity);
        //    DispatchEvents(entity);
        }

        public  void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public async Task<TEntity> FindAsync< TKey>(TKey id) 
        {
            return await _dbContext.FindAsync<TEntity,TKey>(id);
        }

        public IQueryable<TEntity> Where(ISpecification<TEntity> expression)
        {
            return Where(expression.Expression);
        }

        public IQueryable<TEntity> Query()
        {
            return _dbContext.Query<TEntity>();
        }

        public TEntity FindByAsync(Expression<Func<TEntity, bool>> expression)
        {
            return Where(expression).FirstOrDefault();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return Query().Where(expression);
        }

       
    }
}