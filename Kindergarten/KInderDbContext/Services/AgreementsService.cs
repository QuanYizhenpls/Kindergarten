using KinderDbContext.Abstraction;
using KinderData.Entities;
using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Services
{
    /// <summary>
    /// Сервис для выполнения операций с сущностью <see cref="Agreement"/>.
    /// Наследует базовый класс <see cref="BaseService{Agreement}"/> и реализует конкретные методы.
    /// </summary>
    public class AgreementsService : BaseService<Agreement>
    {
        /// <summary>
        /// Получает все сущности <see cref="Agreement"/> из базы данных.
        /// Асинхронно возвращает список договоров.
        /// </summary>
        /// <returns>Задача, содержащая перечисление всех договоров типа <see cref="Agreement"/> или пустую коллекцию.</returns>
        public override async Task<IEnumerable<Agreement?>> GetEntities()
        {
            // Возвращает список всех договоров из базы как IEnumerable
            return await Task.FromResult(ctx.Agreements.ToList() as IEnumerable<Agreement>);
        }

        /// <summary>
        /// Получает конкретный договор по его уникальному идентификатору <see cref="Guid"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор договора.</param>
        /// <returns>Задача, содержащая договор <see cref="Agreement"/>, или null, если не найдено.</returns>
        public override async Task<Agreement?> GetEntity(Guid id)
        {
            // Ищет в базе договор по совпадению ID и возвращает результат
            return await Task.FromResult(ctx.Agreements.SingleOrDefault(a => a.Agreement_Id == id));
        }

        /// <summary>
        /// Добавляет новый договор <see cref="Agreement"/> в базу данных.
        /// </summary>
        /// <param name="entity">Объект договора для добавления.</param>
        /// <returns>Задача, содержащая true, если добавление прошло успешно, иначе false.</returns>
        public override async Task<bool> Add(Agreement entity)
        {
            // Проверка, что объект договора не null
            if (entity == null) return false;

            // Добавление договора в контекст базы данных
            ctx.Agreements.Add(entity);
            // Сохранение изменений в базе данных
            await ctx.SaveChangesAsync();
            // Возвращает true, что операция выполнена успешно
            return true;
        }

        /// <summary>
        /// Обновляет существующий договор <see cref="Agreement"/> на новую версию.
        /// </summary>
        /// <param name="entity">Существующий договор, который нужно обновить.</param>
        /// <param name="newEntity">Объект с обновленными данными.</param>
        /// <returns>Задача, содержащая true, если обновление прошло успешно, иначе false.</returns>
        public override async Task<bool> Update(Agreement entity, Agreement newEntity)
        {
            // Проверка, что оба объекта не null
            if (entity == null || newEntity == null) return false;

            // Обновление полей существующего договора новыми значениями
            entity.Vacation = newEntity.Vacation;
            entity.SickLeave = newEntity.SickLeave;
            entity.Dismissal = newEntity.Dismissal;
            entity.EmploymentContract = newEntity.EmploymentContract;
            entity.Employee = newEntity.Employee;

            // Сохранение изменений в базе данных
            await ctx.SaveChangesAsync();
            // Возврат успешного результата
            return true;
        }

        /// <summary>
        /// Удаляет договор <see cref="Agreement"/> из базы данных.
        /// </summary>
        /// <param name="entity">Объект договора для удаления.</param>
        /// <returns>Задача, содержащая true, если удаление прошло успешно, иначе false.</returns>
        public override async Task<bool> Remove(Agreement entity)
        {
            // Проверка, что объект не null
            if (entity == null) return false;

            // Удаление договора из контекста базы данных
            ctx.Agreements.Remove(entity);
            // Сохраняет изменения в базе
            await ctx.SaveChangesAsync();
            return true;
        }
    }
}
