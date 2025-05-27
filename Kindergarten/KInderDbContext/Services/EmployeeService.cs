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
    /// <summary>
    /// Сервис для управления сущностями <see cref="Employee"/>.
    /// Наследуется от базового класса <see cref="BaseService{Employee}"/> и реализует основные CRUD операции:
    /// получение списка, получение по ID, добавление, обновление и удаление сотрудников.
    /// </summary>
    public class EmployeeService : BaseService<Employee>
    {
        /// <summary>
        /// Асинхронно получает все объекты <see cref="Employee"/> из базы данных.
        /// </summary>
        /// <returns>Задача, возвращающая перечисление всех сотрудников (<see cref="Employee"/>). Может быть пустым.</returns>
        public override async Task<IEnumerable<Employee?>> GetEntities()
        {
            // Получение списка всех сотрудников из контекста и приведение результата в IEnumerable<Employee>
            return await Task.FromResult(ctx.Employees.ToList() as IEnumerable<Employee>);
        }

        /// <summary>
        /// Асинхронно получает одного сотрудника по уникальному идентификатору <see cref="Guid"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сотрудника.</param>
        /// <returns>Задача с объектом <see cref="Employee"/> если найден, или null, если такого сотрудника нет.</returns>
        public override async Task<Employee?> GetEntity(Guid id)
        {
            // Поиск одного сотрудника с заданным идентификатором или возвращение null, если не найден
            return await Task.FromResult(ctx.Employees.SingleOrDefault(e => e.Employee_Id == id));
        }

        /// <summary>
        /// Асинхронно добавляет нового сотрудника в базу данных.
        /// </summary>
        /// <param name="entity">Объект <see cref="Employee"/>, который необходимо добавить.</param>
        /// <returns>Задача, возвращающая true, если объект успешно добавлен, иначе false (например, при null аргументе).</returns>
        public override async Task<bool> Add(Employee entity)
        {
            // Проверка на null - если объект отсутствует, операция отменяется
            if (entity == null) return false;

            // Добавление объекта сотрудника в контекст EF
            ctx.Employees.Add(entity);
            // Асинхронное сохранение изменений в базе данных
            await ctx.SaveChangesAsync();
            // Возврат успешного результата
            return true;
        }

        /// <summary>
        /// Асинхронно обновляет существующего сотрудника новыми данными.
        /// </summary>
        /// <param name="entity">Существующий сотрудник, данные которого нужно обновить.</param>
        /// <param name="newEntity">Объект, содержащий новые данные для обновления.</param>
        /// <returns>Задача, возвращающая true при успешном обновлении, иначе false (если один из объектов null).</returns>
        public override async Task<bool> Update(Employee entity, Employee newEntity)
        {
            // Проверка, что оба объекта не null — иначе обновление невозможно
            if (entity == null || newEntity == null) return false;

            // Обновление свойств сотрудника новыми значениями
            entity.FIO = newEntity.FIO;
            entity.Education = newEntity.Education;
            entity.Experience = newEntity.Experience;
            entity.Post = newEntity.Post;
            entity.Group = newEntity.Group;

            // Асинхронное сохранение изменений в базе данных
            await ctx.SaveChangesAsync();
            // Возврат успеха
            return true;
        }

        /// <summary>
        /// Асинхронно удаляет сотрудника из базы данных.
        /// </summary>
        /// <param name="entity">Объект сотрудника, который необходимо удалить.</param>
        /// <returns>Задача, возвращающая true, если удаление прошло успешно, иначе false (например, при null объекте).</returns>
        public override async Task<bool> Remove(Employee entity)
        {
            if (entity == null) return (false);

            ctx.Employees.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
