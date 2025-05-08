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
    public class GroupService : BaseService<Group>
    {
        public GroupService(AppDbContext context) : base(context) { }

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
            if (entity == null) return await Task.FromResult(false);

            ctx.Groups.Add(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Update(Group entity, Group newEntity)
        {
            if (entity == null || newEntity == null) return await Task.FromResult(false);

            entity.GroupName = newEntity.GroupName;
            entity.Kindergartners = newEntity.Kindergartners;
            entity.Kindergartners = newEntity.Kindergartners;
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Remove(Group entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.Groups.Remove(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }
    }
}
