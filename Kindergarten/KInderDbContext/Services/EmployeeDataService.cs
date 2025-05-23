﻿using KinderData.Entities;
using KinderDbContext.Abstraction;
using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Services
{
    public class EmployeeDataService : BaseService<EmployeeData>
    {

        public override async Task<IEnumerable<EmployeeData?>> GetEntities()
        {
            return await Task.FromResult(ctx.EmployeeDatas.ToList() as IEnumerable<EmployeeData>);
        }

        public override async Task<EmployeeData?> GetEntity(Guid id)
        {
            return await Task.FromResult(ctx.EmployeeDatas.SingleOrDefault(e => e.EmployeeData_Id == id));
        }

        public override async Task<bool> Add(EmployeeData entity)
        {
            if (entity == null) return await Task.FromResult(false);

            ctx.EmployeeDatas.Add(entity);
            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Update(EmployeeData entity, EmployeeData newEntity)
        {
            if (entity == null || newEntity == null) return await Task.FromResult(false);

            entity.Pasport = newEntity.Pasport;
            entity.SNILS = newEntity.SNILS;
            entity.INN = newEntity.INN;
            entity.EmploymentRecord = newEntity.EmploymentRecord;
            entity.Employee = newEntity.Employee;

            await ctx.SaveChangesAsync();
            return await Task.FromResult(true);
        }

        public override async Task<bool> Remove(EmployeeData entity)
        {
            if (entity == null) return (false);

            ctx.EmployeeDatas.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
