using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Abstraction
{
    public abstract class BaseService<T> : IService<T>
    {
        protected readonly AppDbContext ctx; 

        public BaseService(AppDbContext context) 
        {
            ctx = context;
        }

        public abstract Task<IEnumerable<T?>> GetEntities();
        public abstract Task<T?> GetEntity(Guid id);
        public abstract Task<bool> Add(T entity);
        public abstract Task<bool> Update(T entity, T newEntity);
        public abstract Task<bool> Remove(T entity);
    }
}
