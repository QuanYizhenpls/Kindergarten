using KinderData.Entities;
using KinderDbContext.Abstraction;
using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Services
{
    public class PlanService : BaseService<Plan>
    {
        public PlanService(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<Plan?>> GetEntities()
        {
            return await Task.FromResult(ctx.Plans.ToList() as IEnumerable<Plan>);
        }

        public override async Task<Plan?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Plans.SingleOrDefault(e => e.Plan_Id == id));
        }

        public override async Task<bool> Add(Plan entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Plans.Add(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Update(Plan entity, Plan newEntity)
        {
            if (entity == null || newEntity == null) return await Task.FromResult(false);

            entity.DateOfTheEvent = newEntity.DateOfTheEvent;
            entity.Development = newEntity.Development;
            entity.Groups = newEntity.Groups;
            entity.Employees = newEntity.Employees;
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Remove(Plan entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Plans.Remove(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
