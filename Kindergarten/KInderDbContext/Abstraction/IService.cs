using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Abstraction
{
    
        public interface IService<T>
        {
            Task<IEnumerable<T?>> GetEntities();
            Task<T?> GetEntity(Guid id);
            Task<bool> Add(T entity);
            Task<bool> Update(T entity, T newEntity);
            Task<bool> Remove(T entity);
        }
    
}
