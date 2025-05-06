using FoodSupplyInventoryManagementDBContext.Services.Abstraction;
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
    public class AgreementsService : BaseService<Agreements>
    {
        public AgreementsService(AppDbContext context) : base(context) { } 

        public override async Task<IEnumerable<Agreements?>> GetEntities()
        {
            return await Task.FromResult(ctx.Agreements.ToList() as IEnumerable<Agreements>);
        }

        public override async Task<Agreements?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Agreements.SingleOrDefault(a => a.Agreements_Id == id));
        }

        public override async Task<bool> Add(Agreements entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Agreements.Add(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Update(Agreements entity, Agreements newEntity)
        {
            if (entity == null || newEntity == null) return await Task.FromResult(false);

            entity.Vacation = newEntity.Vacation;
            entity.SickLeave = newEntity.SickLeave;
            entity.Dismissal = newEntity.Dismissal;
            entity.EmploymentContract = newEntity.EmploymentContract;
            entity.Employee = newEntity.Employee;

            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Remove(Agreements entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Agreements.Remove(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
