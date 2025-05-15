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
    public class GroupService : BaseService<Group>
    {

        public override async Task<IEnumerable<Group?>> GetEntities()
        {
            return await Task.FromResult(ctx.Groups.ToList() as IEnumerable<Group>);
        }

        public override async Task<Group?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.Groups.SingleOrDefault(e => e.Group_Id == id));
        }

        public override async Task<bool> Add(Group entity)
        {
            if (entity == null) return (false);

            ctx.Groups.Add(entity);
            await ctx.SaveChangesAsync();
            return (true);
        }

        public override async Task<bool> Update(Group entity, Group newEntity)
        {
            if (entity == null || newEntity == null) return (false);

            entity.GroupName = newEntity.GroupName;
            entity.Kindergartner = newEntity.Kindergartner;
            await ctx.SaveChangesAsync();
            return (true);
        }

        public override async Task<bool> Remove(Group entity)
        {
            if (entity == null) return (false);

            ctx.Groups.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
