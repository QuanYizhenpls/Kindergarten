using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Abstraction
{
    /// <summary>
    /// Интерфейс для управления учетными записями пользователя.
    /// Определяет контракт для получения аккаунта по логину и паролю.
    /// </summary>
    /// <typeparam name="T">Тип сущности учетной записи (например, пользователь).</typeparam>
    public interface IAccountManagement<T>
    {
        /// <summary>
        /// Асинхронно получает учетную запись пользователя по логину и паролю.
        /// Обычно используется для аутентификации.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Объект учетной записи типа T, если найден; иначе null.</returns>
        public Task<T?> GetAccount(string login, string password);
    }
}
