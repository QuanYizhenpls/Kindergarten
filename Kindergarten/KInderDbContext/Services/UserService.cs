using KinderDbContext.Abstraction;
using KinderData.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KinderDbContext.Connections;

namespace KinderDbContext.Services
{
    /// <summary>
    /// Сервис управления пользователями, реализующий операции добавления, получения и обновления пользователя.
    /// Наследует от базового <see cref="DbEntityServiceBase{User}"/> и реализует интерфейс <see cref="IAccountManagement{User}"/>.
    /// </summary>
    public class UserService : DbEntityServiceBase<User>, IAccountManagement<User>
    {
        /// <summary>
        /// Асинхронно добавляет нового пользователя в базу данных после проверки данных.
        /// Выполняет проверки на null, корректность Guid, заполненность ФИО и логина с паролем.
        /// </summary>
        /// <param name="entity">Экземпляр <see cref="User"/>, который необходимо добавить.</param>
        /// <returns>
        /// Задача, возвращающая true, если пользователь успешно добавлен;
        /// false в случае ошибок, например, если входной объект null или содержит некорректные данные.
        /// </returns>
        public override async Task<bool> Add(User entity)
        {
            // Проверка, что объект не null
            if (entity == null) return await Task.FromResult(false);

            // Проверка, что Id не является пустым Guid
            if (entity.Id == Guid.Empty) return await Task.FromResult(false);

            // Проверка, что имя не пустое или null
            if (string.IsNullOrEmpty(entity.Firstname)) return await Task.FromResult(false);
            // Проверка, что фамилия не пустая или null
            if (string.IsNullOrEmpty(entity.Lastname)) return await Task.FromResult(false);

            // Проверка, что логин не пустой и не null
            if (string.IsNullOrEmpty(entity.Login)) return await Task.FromResult(false);
            // Проверка, что пароль не пустой и не null
            if (string.IsNullOrEmpty(entity.Password)) return await Task.FromResult(false);

            // Попытка вставить пользователя в базу данных
            try
            {
                // Асинхронное добавление объекта в контекст
                await ctx.AddAsync(entity);
                // Вывод в отладочную строку о добавлении
                Debug.WriteLine($"{GetType().Name}: пользователь добавлен!");
                // Синхронное сохранение изменений (можно заменить на асинхронное)
                ctx.SaveChanges();
                Debug.WriteLine($"{GetType().Name}: пользователь сохранён!");
            }
            catch (Exception ex)
            {
                // Логирование ошибок
                Debug.WriteLine($"{GetType().Name}: {ex.Message}");
                // Возврат false, если произошло исключение
                return await Task.FromResult(false);
            }

            // Возвращаем true, если операция прошла успешно
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Асинхронно получает всех пользователей из базы данных.
        /// </summary>
        /// <returns>
        /// Задача, возвращающая перечисление всех объектов <see cref="User"/>, или пустой список.
        /// </returns>
        public override async Task<IEnumerable<User?>> GetEntities()
            => await Task.FromResult(ctx.Users.ToList() as IEnumerable<User>);

        /// <summary>
        /// Асинхронно ищет пользователя по логину и паролю.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>
        /// Задача, возвращающая объект <see cref="User"/>, соответствующий логину и паролю, или null, если не найден.
        /// </returns>
        public async Task<User?> GetAccount(string login, string password)
            => await Task.FromResult(ctx.Users.SingleOrDefault(c => c.Login == login && c.Password == password));

        /// <summary>
        /// Асинхронно получает пользователя по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор <see cref="Guid"/>.</param>/// <returns>
        /// Задача, возвращающая объект <see cref="User"/> с заданным id или null, если пользователь не найден.
        /// </returns>
        public override async Task<User?> GetEntity(Guid id)
            => await Task.FromResult(ctx.Users.Single(c => c.Id == id));

        /// <summary>
        /// Асинхронно обновляет существующего пользователя новыми данными.
        /// Выполняет проверки на null, корректность Guid, заполненность ФИО, логина и пароля.
        /// </summary>
        /// <param name="entity">Объект существующего пользователя, подлежащий обновлению.</param>
        /// <param name="newEntity">Объект с новыми данными для обновления.</param>
        /// <returns>
        /// Задача, возвращающая true, если обновление прошло успешно; 
        /// иначе - false при ошибках.
        /// </returns>
        public override async Task<bool> Update(User entity, User newEntity)
        {
            // Проверка, что исходный объект не null
            if (entity == null) return await Task.FromResult(false);
            // Проверка, что новый объект не null
            if (newEntity == null) return await Task.FromResult(false);

            // Проверка, что Id нового объекта не пустой Guid
            if (newEntity.Id == Guid.Empty) return await Task.FromResult(false);

            // Проверка, что новое имя не пустое
            if (string.IsNullOrEmpty(newEntity.Firstname)) return await Task.FromResult(false);
            // Проверка, что новая фамилия не пустая
            if (string.IsNullOrEmpty(newEntity.Lastname)) return await Task.FromResult(false);

            // Проверка, что логин не пустой
            if (string.IsNullOrEmpty(newEntity.Login)) return await Task.FromResult(false);
            // Проверка, что пароль не пустой
            if (string.IsNullOrEmpty(newEntity.Password)) return await Task.FromResult(false);

            // Обновление свойств исходного пользователя новыми значениями
            try
            {
                entity.Firstname = newEntity.Firstname;
                entity.Lastname = newEntity.Lastname;
                entity.Middlename = newEntity.Middlename;
                entity.Login = newEntity.Login;
                entity.Password = newEntity.Password;

                // Обновление данных в контексте EF
                ctx.Update(entity);
                // Вывод в отладочную строку о успешном обновлении
                Debug.WriteLine($"{GetType().Name}: пользователь обновлен!");
                // Сохранение изменений
                ctx.SaveChanges();
                Debug.WriteLine($"{GetType().Name}: изменения сохранены!");
            }
            catch (Exception ex)
            {
                // Логирование исключений
                Debug.WriteLine($"{GetType().Name}: {ex.Message}");
                // Возвращение false при ошибке
                return await Task.FromResult(false);
            }

            // Возврат true после успешного обновления
            return await Task.FromResult(true);
        }
    }
}
