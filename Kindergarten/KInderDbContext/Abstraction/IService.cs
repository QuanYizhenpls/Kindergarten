using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Abstraction
{
    /// <summary>
    /// Интерфейс сервиса работы с сущностями типа T.
    /// Определяет базовый набор CRUD операций.
    /// Может повторять контракт <see cref="IBaseManagement{T}"/>, но применяется в другом контексте.
    /// </summary>
    /// <typeparam name="T">Тип сущности.</typeparam>
    public interface IService<T>
    {
        /// <summary>
        /// Асинхронно получает все сущности типа T.
        /// </summary>
        /// <returns>Перечисление сущностей или пустая коллекция.</returns>
        Task<IEnumerable<T?>> GetEntities();

        /// <summary>
        /// Асинхронно получает сущность по уникальному идентификатору.
        /// </summary>
        /// <param name="id">GUID сущности.</param>
        /// <returns>Объект сущности или null.</returns>
        Task<T?> GetEntity(Guid id);

        /// <summary>
        /// Асинхронно добавляет сущность.
        /// </summary>
        /// <param name="entity">Сущность для добавления.</param>
        /// <returns>True в случае успеха, иначе false.</returns>
        Task<bool> Add(T entity);

        /// <summary>
        /// Асинхронно обновляет сущность.
        /// </summary>
        /// <param name="entity">Старая сущность.</param>
        /// <param name="newEntity">Новая сущность с обновлёнными данными.</param>
        /// <returns>True при успешном обновлении, иначе false.</returns>
        Task<bool> Update(T entity, T newEntity);
        /// <summary>
        /// Асинхронно удаляет сущность.
        /// </summary>
        /// <param name="entity">Сущность для удаления.</param>
        /// <returns>True в случае успешного удаления, иначе false.</returns>
        Task<bool> Remove(T entity);
    }

}
