using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Abstraction
{
    /// <summary>
    /// Интерфейс базового управления сущностями типа T.
    /// Обеспечивает стандартные операции CRUD.
    /// </summary>
    /// <typeparam name="T">Тип управляемой сущности.</typeparam>
    public interface IBaseManagement<T>
    {
        /// <summary>
        /// Асинхронно получает все сущности типа T.
        /// </summary>
        /// <returns>Коллекция всех сущностей типа T или пустая коллекция.</returns>
        public Task<IEnumerable<T?>> GetEntities();

        /// <summary>
        /// Асинхронно получает конкретную сущность по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности.</param>
        /// <returns>Объект сущности типа T или null, если сущность не найдена.</returns>
        public Task<T?> GetEntity(Guid id);

        /// <summary>
        /// Асинхронно добавляет новую сущность.
        /// </summary>
        /// <param name="entity">Добавляемая сущность.</param>
        /// <returns>True, если добавление прошло успешно; иначе false.</returns>
        public Task<bool> Add(T entity);

        /// <summary>
        /// Асинхронно обновляет существующую сущность на новую.
        /// </summary>
        /// <param name="entity">Существующая сущность, которая будет обновлена.</param>
        /// <param name="newEntity">Новая сущность с обновленными данными.</param>
        /// <returns>True, если обновление прошло успешно; иначе false.</returns>
        public Task<bool> Update(T entity, T newEntity);

        /// <summary>
        /// Асинхронно удаляет сущность.
        /// </summary>
        /// <param name="entity">Удаляемая сущность.</param>
        /// <returns>True, если удаление прошло успешно; иначе false.</returns>
        public Task<bool> Remove(T entity);
    }
}
