using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Abstraction
{
    /// <summary>
    /// Абстрактный базовый класс для управления сущностями T с реализацией базовых методов по работе с Entity Framework.
    /// Реализует интерфейс <see cref="IBaseManagement{T}"/> и включает базовую логику удаления.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public abstract class DbEntityServiceBase<T> : IBaseManagement<T>
    {
        /// <summary>
        /// Контекст базы данных для всех операций.
        /// Используется для взаимодействия с базой данных.
        /// </summary>
        protected readonly AppDbContext ctx;

        /// <summary>
        /// Конструктор сервиса.
        /// Инициализирует контекст базы данных через синглтон.
        /// </summary>
        public DbEntityServiceBase()
        {
            // Получение экземпляра контекста базы данных через синглтон
            ctx = DbContextSingleton.Instance.DbContext;
        }

        /// <summary>
        /// Абстрактный метод для получения всех сущностей T.
        /// Должна быть реализована в наследуемом классе.
        /// </summary>
        /// <returns>Асинхронное перечисление сущностей.</returns>
        public abstract Task<IEnumerable<T?>> GetEntities();
        /// <summary>
        /// Абстрактный метод для получения конкретной сущности по GUID.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности.</param>
        /// <returns>Асинхронная задача, содержащая объект T или null.</returns>
        public abstract Task<T?> GetEntity(Guid id);

        /// <summary>
        /// Абстрактный метод для добавления новой сущности в базу.
        /// </summary>
        /// <param name="entity">Объект-сущность для добавления.</param>
        /// <returns>Результат операции в виде задачи с булевым значением успеха.</returns>
        public abstract Task<bool> Add(T entity);

        /// <summary>
        /// Абстрактный метод для обновления существующей сущности.
        /// </summary>
        /// <param name="entity">Объект, который нужно обновить.</param>
        /// <param name="newEntity">Объект с обновленными данными.</param>
        /// <returns>Результат операции в виде задачи с булевым значением.</returns>
        public abstract Task<bool> Update(T entity, T newEntity);

        /// <summary>
        /// Виртуальный метод для удаления сущности.
        /// Может быть переопределен в наследуемых классах.
        /// В базовой реализации осуществляется удаление из базы с обработкой исключений.
        /// </summary>
        /// <param name="entity">Объект для удаления.</param>
        /// <returns>Асинхронная задача, содержащая результат операции (успех или неудача).</returns>
        public async virtual Task<bool> Remove(T entity)
        {
            // Проверка, что объект не null
            if (entity == null) return await Task.FromResult(false);

            // Обработка удаления из базы данных с помощью Entity Framework
            try
            {
                // Удаление объекта из контекста
                ctx.Remove(entity);
                // Вывод в отладочную консоль
                Debug.WriteLine($"{GetType().Name}: entity removed!");
                // Сохранение изменений в базе данных
                await ctx.SaveChangesAsync();
                Debug.WriteLine($"{GetType().Name}: changes saved!");
            }
            catch (Exception ex)
            {
                // В случае ошибки вывод сообщения об исключении
                Debug.WriteLine($"{GetType().Name}: {ex.Message}");
                return false; // Возвращается false при возникновении исключения
            }

            // Если удаление прошло успешно, возвращается true
            return await Task.FromResult(true);
        }
    }
}
