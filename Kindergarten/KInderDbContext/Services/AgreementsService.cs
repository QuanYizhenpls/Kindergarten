using KinderDbContext.Abstraction;
using KinderData.Entities;
using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderData.Services
{
    public class AgreementsService : BaseService<Agreement>
    {
        public AgreementsService(AppDbContext context) : base(context) { } 

        public override async Task<IEnumerable<Agreement?>> GetEntities()
        {
            return await Task.FromResult(ctx.Agreements.ToList() as IEnumerable<Agreement>);
        }

        public override async Task<Agreement?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Agreements.SingleOrDefault(a => a.Agreement_Id == id));
        }

        public override async Task<bool> Add(Agreement entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Agreements.Add(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Update(Agreement entity, Agreement newEntity)
        {
            if (entity == null || newEntity == null) return await Task.FromResult(false);

            entity.Vacation = newEntity.Vacation;
            entity.SickLeave = newEntity.SickLeave;
            entity.Dismissal = newEntity.Dismissal;
            entity.EmploymentContract = newEntity.EmploymentContract;
            entity.Employees = newEntity.Employees;

            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Remove(Agreement entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Agreements.Remove(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
