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
    /// <summary>
    /// Сервис для управления сущностями <see cref="Plan"/>.
    /// Наследуется от базового класса <see cref="BaseService{Plan}"/> и реализует основные операции:
    /// получение списка, получение по идентификатору, добавление, обновление и удаление плана.
    /// </summary>
    public class PlanService : BaseService<Plan>
    {
        /// <summary>
        /// Асинхронно получает все объекты <see cref="Plan"/> из базы данных,
        /// включая связанные сущности <see cref="Employee"/> и <see cref="Group"/> через навигационные свойства.
        /// </summary>
        /// <returns>
        /// Задача, возвращающая перечисление всех планов с их сотрудниками и группами.
        /// Может вернуть пустую коллекцию, если объектов нет.
        /// </returns>
        public override async Task<IEnumerable<Plan?>> GetEntities()
        {
            // Получаем все планы с включёнными связанными сущностями Employee и Group,
            // чтобы избежать ленивой загрузки и уменьшить количество запросов к базе
            return await Task.FromResult(ctx.Plans
                .Include(p => p.Employee)               // Включаем сотрудника плана
                .ThenInclude(e => e.Group)              // Включаем группу сотрудника
                .ToList() as IEnumerable<Plan>);
        }

        /// <summary>
        /// Асинхронно получает один план по уникальному идентификатору <see cref="Guid"/>,
        /// включая связанные объекты сотрудника и группы.
        /// </summary>
        /// <param name="id">Уникальный идентификатор плана.</param>
        /// <returns>
        /// Задача, возвращающая объект <see cref="Plan"/> с навигационными свойствами,
        /// или null, если план с таким идентификатором не найден.
        /// </returns>
        public override async Task<Plan?> GetEntity(Guid id)
        {
            // Поиск плана по идентификатору с включением связанных Employee и Group
            return await Task.FromResult(ctx.Plans
                .Include(p => p.Employee)
                .ThenInclude(e => e.Group)
                .SingleOrDefault(e => e.Plan_Id == id));
        }

        /// <summary>
        /// Асинхронно добавляет новый объект <see cref="Plan"/> в базу данных.
        /// </summary>
        /// <param name="entity">Объект плана для добавления.</param>
        /// <returns>
        /// Задача, возвращающая true, если объект успешно добавлен;
        /// false, если переданный объект null и добавление невозможно.
        /// </returns>
        public override async Task<bool> Add(Plan entity)
        {
            // Проверка на null чтобы избежать исключений
            if (entity == null) return false;

            // Добавление нового плана в контекст EF
            ctx.Plans.Add(entity);

            // Асинхронное сохранение изменений в базе данных
            await ctx.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Асинхронно обновляет существующий объект плана новыми данными.
        /// </summary>
        /// <param name="entity">Существующий объект плана, подлежащий обновлению.</param>
        /// <param name="newEntity">Объект с новыми значениями полей.</param>
        /// <returns>
        /// Задача, возвращающая true при успешном обновлении;
        /// false, если один из аргументов null.
        /// </returns>
        public override async Task<bool> Update(Plan entity, Plan newEntity)
        {
            // Проверка обоих объектов на null
            if (entity == null || newEntity == null) return false;

            // Обновление свойств существующего плана новыми значениями
            entity.DateOfTheEvent = newEntity.DateOfTheEvent;
            entity.Development = newEntity.Development;
            entity.Employee = newEntity.Employee;

            // Асинхронное сохранение изменений в базе данных
            await ctx.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Асинхронно удаляет существующий объект плана из базы данных.
        /// </summary>
        /// <param name="entity">Объект плана для удаления.</param>
        /// <returns>
        /// Задача, возвращающая true, если удаление прошло успешно;
        /// false, если переданный объект null и удаление невозможно выполнить.
        /// </returns>
        public override async Task<bool> Remove(Plan entity)
        {
            if (entity == null) return (false);

            ctx.Plans.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
