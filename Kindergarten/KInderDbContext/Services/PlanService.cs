using KinderData.Entities;
using KinderDbContext.Abstraction;
using KinderDbContext.Connections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Services
{
    public class PlanService : BaseService<Plan>
    {

        public override async Task<IEnumerable<Plan?>> GetEntities()
        {
            return await Task.FromResult(ctx.Plans.Include(p=>p.Employee).ThenInclude(e=>e.Group).ToList() as IEnumerable<Plan>);
        }

        public override async Task<Plan?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Plans.Include(p => p.Employee).ThenInclude(e => e.Group).SingleOrDefault(e => e.Plan_Id == id));
        }

        public override async Task<bool> Add(Plan entity)
        {
            if (entity == null) return (false);

            ctx.Plans.Add(entity);
            await ctx.SaveChangesAsync();
            return (true);
        }

        public override async Task<bool> Update(Plan entity, Plan newEntity)
        {
            if (entity == null || newEntity == null) return (false);

            entity.DateOfTheEvent = newEntity.DateOfTheEvent;
            entity.Development = newEntity.Development;
            entity.Employee = newEntity.Employee;
            await ctx.SaveChangesAsync();
            return (true);
        }

        public override async Task<bool> Remove(Plan entity)
        {
            if (entity == null) return (false);

            ctx.Plans.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
