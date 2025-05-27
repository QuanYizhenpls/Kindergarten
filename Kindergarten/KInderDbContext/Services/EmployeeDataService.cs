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
    /// Сервис для выполнения операций с сущностью <see cref="EmployeeData"/>.
    /// Наследует базовый класс <see cref="BaseService{EmployeeData}"/> и реализует конкретные методы для работы с данными работников.
    /// </summary>
    public class EmployeeDataService : BaseService<EmployeeData>
    {
        /// <summary>
        /// Получает все сущности <see cref="EmployeeData"/> из базы данных.
        /// Асинхронно возвращает список данных работников.
        /// </summary>
        /// <returns>Задача, содержащая перечисление всех объектов <see cref="EmployeeData"/> или пустую коллекцию.</returns>
        public override async Task<IEnumerable<EmployeeData?>> GetEntities()
        {
            // Возвращает список всех данных работников из базы, преобразуя его в IEnumerable<EmployeeData>
            return await Task.FromResult(ctx.EmployeeDatas.ToList() as IEnumerable<EmployeeData>);
        }

        /// <summary>
        /// Получает конкретный объект <see cref="EmployeeData"/> по его уникальному идентификатору <see cref="Guid"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор данных работника.</param>
        /// <returns>Задача, содержащая соответствующий объект <see cref="EmployeeData"/>, или null, если не найдено.</returns>
        public override async Task<EmployeeData?> GetEntity(Guid id)
        {
            // Ищет в базе данных запись по ID и возвращает ее, или null, если не найдено
            return await Task.FromResult(ctx.EmployeeDatas.SingleOrDefault(e => e.EmployeeData_Id == id));
        }

        /// <summary>
        /// Добавляет новый объект <see cref="EmployeeData"/> в базу данных.
        /// </summary>
        /// <param name="entity">Объект данных работника для добавления.</param>
        /// <returns>Задача, содержащая true, если добавление успешно, иначе false.</returns>
        public override async Task<bool> Add(EmployeeData entity)
        {
            // Проверка, что объект для добавления не null
            if (entity == null) return await Task.FromResult(false);

            // Добавление объекта в контекст базы данных
            ctx.EmployeeDatas.Add(entity);
            // Асинхронное сохранение изменений
            await ctx.SaveChangesAsync();
            // Возвращение true, что операция завершена успешно
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Обновляет существующий <see cref="EmployeeData"/> на новую версию.
        /// </summary>
        /// <param name="entity">Объект, который нужно обновить.</param>
        /// <param name="newEntity">Объект с обновленными данными.</param>
        /// <returns>Задача, содержащая true, если обновление прошло успешно, иначе false.</returns>
        public override async Task<bool> Update(EmployeeData entity, EmployeeData newEntity)
        {
            // Проверка, что оба объекта не null
            if (entity == null || newEntity == null) return await Task.FromResult(false);

            // Обновление полей объекта
            entity.Pasport = newEntity.Pasport;
            entity.SNILS = newEntity.SNILS;
            entity.INN = newEntity.INN;
            entity.EmploymentRecord = newEntity.EmploymentRecord;
            entity.Employee = newEntity.Employee;

            // Асинхронное сохранение изменений
            await ctx.SaveChangesAsync();
            // Возврат успеха
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Удаляет объект <see cref="EmployeeData"/> из базы данных.
        /// </summary>
        /// <param name="entity">Объект данных работника для удаления.</param>
        /// <returns>Задача, содержащая true, если удаление прошло успешно, иначе false.</returns>
        public override async Task<bool> Remove(EmployeeData entity)
        {
            // Проверка, что объект не null
            if (entity == null) return false;
            ctx.EmployeeDatas.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
