using KinderData.Entities;
using KinderDbContext.Abstraction;
using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Services
{
    public class KindergartnerService : BaseService<Kindergartner>
    {

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
            if (entity == null) return (false);

            ctx.Kindergartners.Add(entity);
            await ctx.SaveChangesAsync();
            return  (true);
        }

        public override async Task<bool> Update(Kindergartner entity, Kindergartner newEntity)
        {
            if (entity == null || newEntity == null) return (false);

            entity.FIO = newEntity.FIO;
            entity.DateOfBirth = newEntity.DateOfBirth;
            entity.ParentsContactInfo = newEntity.ParentsContactInfo;
            entity.Group = newEntity.Group;
            ctx.Kindergartners.Update(entity);
            ctx.SaveChanges();
            return (true);
        }

        public override async Task<bool> Remove(Kindergartner entity)
        {
            if (entity == null) return (false);

            ctx.Kindergartners.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
