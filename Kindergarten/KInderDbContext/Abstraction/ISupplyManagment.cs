using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Abstraction
{
    /// <summary>
    /// Интерфейс для управления поставками и их статусами.
    /// Предоставляет методы для установки различных статусов для сущности.
    /// </summary>
    /// <typeparam name="T">Тип сущности поставки.</typeparam>
    public interface ISupplyManagment<T>
    {
        /// <summary>
        /// Устанавливает статус сущности как "Ожидание" (Waiting).
        /// </summary>
        /// <param name="entity">Сущность, для которой устанавливается статус.</param>
        /// <returns>True, если статус успешно установлен; иначе false.</returns>
        public Task<bool> SetStatusWaiting(T entity);

        /// <summary>
        /// Устанавливает статус сущности как "В процессе" (In Progress).
        /// </summary>
        /// <param name="entity">Сущность, для которой устанавливается статус.</param>
        /// <returns>True, если статус успешно установлен; иначе false.</returns>
        public Task<bool> SetStatusInProgress(T entity);

        /// <summary>
        /// Устанавливает статус сущности как "Завершено" (Completed).
        /// </summary>
        /// <param name="entity">Сущность, для которой устанавливается статус.</param>
        /// <returns>True, если статус успешно установлен; иначе false.</returns>
        public Task<bool> SetStatusCompleted(T entity);

        /// <summary>
        /// Устанавливает статус сущности как "Отменено" (Canceled).
        /// </summary>
        /// <param name="entity">Сущность, для которой устанавливается статус.</param>
        /// <returns>True, если статус успешно установлен; иначе false.</returns>
        public Task<bool> SetStatusCanceled(T entity);
    }
}
