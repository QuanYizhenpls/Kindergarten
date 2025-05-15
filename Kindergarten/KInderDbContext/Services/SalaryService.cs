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
    public class SalaryService : BaseService<Salary>
    {

        public override async Task<IEnumerable<Salary?>> GetEntities()
        {
            return await Task.FromResult(ctx.Salaries.ToList() as IEnumerable<Salary>);
        }

        public override async Task<Salary?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Salaries.SingleOrDefault(e => e.Salary_Id == id));
        }

        public override async Task<bool> Add(Salary entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Salaries.Add(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Update(Salary entity, Salary newEntity)
        {
            if (entity == null || newEntity == null) return await Task.FromResult(false);

            entity.Wage = newEntity.Wage;
            entity.Bonus = newEntity.Bonus;
            entity.Allowance = newEntity.Allowance;
            entity.Prepayment = newEntity.Prepayment;
            entity.Penalty = newEntity.Penalty;
            entity.Employee = newEntity.Employee;
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Remove(Salary entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Salaries.Remove(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
