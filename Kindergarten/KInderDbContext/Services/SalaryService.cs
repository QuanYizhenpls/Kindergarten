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
    /// Сервис для управления сущностями <see cref="Salary"/>.
    /// Наследуется от базового класса <see cref="BaseService{Salary}"/> и реализует асинхронные операции:
    /// получение списка зарплат, получение по идентификатору, добавление, обновление и удаление записи зарплаты.
    /// </summary>
    public class SalaryService : BaseService<Salary>
    {
        /// <summary>
        /// Асинхронно получает все объекты <see cref="Salary"/> из базы данных.
        /// </summary>
        /// <returns>
        /// Задача, возвращающая перечисление всех зарплат
        /// (может содержать null-элементы, если в уточнении метода предусмотрено Nullable).
        /// </returns>
        public override async Task<IEnumerable<Salary?>> GetEntities()
        {
            // Получение всех зарплат из контекста EF и преобразование результата в IEnumerable<Salary>
            return await Task.FromResult(ctx.Salaries.ToList() as IEnumerable<Salary>);
        }

        /// <summary>
        /// Асинхронно получает один объект <see cref="Salary"/> по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор зарплаты.</param>
        /// <returns>
        /// Задача, возвращающая объект зарплаты с заданным идентификатором или null,
        /// если запись не найденa.
        /// </returns>
        public override async Task<Salary?> GetEntity(Guid id)
        {
            // Поиск зарплаты по заданному ID или возврат null, если такой записи нет
            return await Task.FromResult(ctx.Salaries.SingleOrDefault(e => e.Salary_Id == id));
        }

        /// <summary>
        /// Асинхронно добавляет новую запись <see cref="Salary"/> в базу данных.
        /// </summary>
        /// <param name="entity">Экземпляр зарплаты, который необходимо добавить.</param>
        /// <returns>
        /// Задача, возвращающая true при успешном добавлении;
        /// false при передаче null объекта для добавления.
        /// </returns>
        public override async Task<bool> Add(Salary entity)
        {
            // Проверяем, что объект для добавления не null
            if (entity == null) return false;

            // Добавление объекта в контекст EF 
            ctx.Salaries.Add(entity);
            // Асинхронное сохранение изменений в базе данных
            await ctx.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Асинхронно обновляет существующую запись <see cref="Salary"/> новыми значениями.
        /// </summary>
        /// <param name="entity">Существующая запись для обновления.</param>
        /// <param name="newEntity">Объект с новыми значениями свойств.</param>
        /// <returns>
        /// Задача, возвращающая true при успешном обновлении;
        /// false если один из объектов null и обновление невозможно.
        /// </returns>
        public override async Task<bool> Update(Salary entity, Salary newEntity)
        {
            // Проверка на null переданных объектов
            if (entity == null || newEntity == null) return false;

            // Обновление свойств существующего объекта новыми значениями
            entity.Wage = newEntity.Wage;
            entity.Bonus = newEntity.Bonus;
            entity.Allowance = newEntity.Allowance;
            entity.Prepayment = newEntity.Prepayment;
            entity.Penalty = newEntity.Penalty;
            entity.Employee = newEntity.Employee;

            // Асинхронное сохранение изменений в базе
            await ctx.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Асинхронно удаляет запись <see cref="Salary"/> из базы данных.
        /// </summary>
        /// <param name="entity">Объект зарплаты для удаления.</param>
        /// <returns>
        /// Задача, возвращающая true при успешном удалении;
        /// false если переданный объект null и удаление невозможно.
        /// </returns>
        public override async Task<bool> Remove(Salary entity)
        {
            if (entity == null) return (false);

            ctx.Salaries.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
