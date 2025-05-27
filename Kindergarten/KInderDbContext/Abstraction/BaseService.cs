using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Abstraction
{
    /// <summary>
    /// Абстрактный базовый класс сервиса, предназначенный для реализации операций над сущностями T.
    /// Реализует интерфейс <see cref="IService{T}"/> и служит базой для конкретных сервисов.
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает сервис.</typeparam>
    public abstract class BaseService<T> : IService<T>
    {
        /// <summary>
        /// Контекст базы данных для операций с данными.
        /// Используется для взаимодействия с базой данных через Entity Framework.
        /// </summary>
        protected readonly AppDbContext ctx;

        /// <summary>
        /// Конструктор базового сервиса.
        /// Инициализирует контекст базы данных singleton'ом.
        /// </summary>
        public BaseService()
        {
            // Получение экземпляра контекста базы данных через синглтон
            ctx = DbContextSingleton.Instance.DbContext;
        }

        /// <summary>
        /// Абстрактный метод для получения всех сущностей T.
        /// Реализация должна возвращать перечисление сущностей асинхронно.
        /// </summary>
        /// <returns>Асинхронная задача, содержащая перечисление сущностей T или null.</returns>
        public abstract Task<IEnumerable<T?>> GetEntities();

        /// <summary>
        /// Абстрактный метод для получения конкретной сущности по её уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности.</param>
        /// <returns>Асинхронная задача, содержащая объект сущности T или null, если не найдено.</returns>
        public abstract Task<T?> GetEntity(Guid id);

        /// <summary>
        /// Абстрактный метод для добавления новой сущности T.
        /// Реализация должна сохранять новую сущность в базу.
        /// </summary>
        /// <param name="entity">Объект сущности для добавления.</param>
        /// <returns>Асинхронная задача, являющаяся результатом операции добавления (успех или неудача).</returns>
        public abstract Task<bool> Add(T entity);

        /// <summary>
        /// Абстрактный метод для обновления существующей сущности.
        /// </summary>
        /// <param name="entity">Старый объект-сущность, которую нужно обновить.</param>
        /// <param name="newEntity">Новый объект-сущность с измененными данными.</param>
        /// <returns>Асинхронная задача, содержащая результат операции обновления (успех или неудача).</returns>
        public abstract Task<bool> Update(T entity, T newEntity);

        /// <summary>
        /// Абстрактный метод для удаления сущности.
        /// </summary>
        /// <param name="entity">Объект-сущность для удаления.</param>
        /// <returns>Асинхронная задача, являющаяся результатом операции удаления (успех или неудача).</returns>
        public abstract Task<bool> Remove(T entity);
    }
}
