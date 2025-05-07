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
    public class KindergartnerService : BaseService<Kindergartner>
    {
        public KindergartnerService(AppDbContext context) : base(context) { }

        public override async Task<IEnumerable<Kindergartner?>> GetEntities()
        {
            return await Task.FromResult(ctx.Kindergartners.ToList() as IEnumerable<Kindergartner>);
        }

        public override async Task<Kindergartner?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Kindergartners.SingleOrDefault(e => e.Kindergartner_Id == id));
        }

        public override async Task<bool> Add(Kindergartner entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Kindergartners.Add(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Update(Kindergartner entity, Kindergartner newEntity)
        {
            if (entity == null || newEntity == null) return await Task.FromResult(false);

            entity.FIO = newEntity.FIO;
            entity.DateOfBirth = newEntity.DateOfBirth;
            entity.ParentsContactInfo = newEntity.ParentsContactInfo;
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Remove(Kindergartner entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Kindergartners.Remove(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
