using KinderData.Entities;
using KinderDbContext.Abstraction;
using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Services
{
    public class EmployeeService : BaseService<Employee>
    {
        public EmployeeService(AppDbContext context) : base(context) { } 

        public override async Task<IEnumerable<Employee?>> GetEntities()
        {
            return await Task.FromResult(ctx.Employees.ToList() as IEnumerable<Employee>);
        }

        public override async Task<Employee?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Employees.SingleOrDefault(e => e.Employee_Id == id));
        }

        public override async Task<bool> Add(Employee entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Employees.Add(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Update(Employee entity, Employee newEntity)
        {
            if (entity == null || newEntity == null) return await Task.FromResult(false);

            entity.FIO = newEntity.FIO;
            entity.Education = newEntity.Education;
            entity.Experience = newEntity.Experience;
            entity.Post = newEntity.Post;
            entity.Groups = newEntity.Groups;

            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Remove(Employee entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Employees.Remove(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
