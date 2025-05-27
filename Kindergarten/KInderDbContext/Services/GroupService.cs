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
    /// Сервис для работы с сущностью <see cref="Group"/>.
    /// Наследует базовый класс <see cref="BaseService{Group}"/> и реализует CRUD-операции (создание, чтение, обновление, удаление)
    /// для управления группами в базе данных.
    /// </summary>
    public class GroupService : BaseService<Group>
    {
        /// <summary>
        /// Асинхронно получает все объекты <see cref="Group"/> из контекста базы данных.
        /// </summary>
        /// <returns>
        /// Задача, возвращающая перечисление всех групп из базы данных.
        /// Может содержать пустую коллекцию, если групп нет.
        /// </returns>
        public override async Task<IEnumerable<Group?>> GetEntities()
        {
            // Получение списка всех групп и приведение к IEnumerable<Group>
            return await Task.FromResult(ctx.Groups.ToList() as IEnumerable<Group>);
        }

        /// <summary>
        /// Асинхронно ищет и возвращает объект <see cref="Group"/> по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор группы типа <see cref="Guid"/>.</param>
        /// <returns>Задача, возвращающая найденный объект группы или null, если группа не найдена.</returns>
        public override async Task<Group?> GetEntity(Guid id)
        {
            // Поиск группы по идентификатору, возвращает null если не найдено
            return await Task.FromResult(ctx.Groups.SingleOrDefault(e => e.Group_Id == id));
        }

        /// <summary>
        /// Добавляет новый объект <see cref="Group"/> в базу данных.
        /// </summary>
        /// <param name="entity">Объект группы, который нужно добавить.</param>
        /// <returns>Задача, возвращающая true, если группа добавлена успешно, иначе false при null входном значении.</returns>
        public override async Task<bool> Add(Group entity)
        {
            // Проверяем, что объект не null
            if (entity == null) return false;

            // Добавление группы в контекст базы данных
            ctx.Groups.Add(entity);
            // Сохраняем изменения синхронно
            ctx.SaveChanges();
            // Возврат успешного результата
            return true;
        }

        /// <summary>
        /// Обновляет данные существующей группы <see cref="Group"/>.
        /// </summary>
        /// <param name="entity">Существующая группа, которую необходимо обновить.</param>
        /// <param name="newEntity">Объект с новыми значениями свойств группы.</param>
        /// <returns>Задача, возвращающая true при успешном обновлении, иначе false, если один из объектов null.</returns>
        public override async Task<bool> Update(Group entity, Group newEntity)
        {
            // Проверка, что оба объекта не null
            if (entity == null || newEntity == null) return false;

            // Обновление имени группы новым значением
            entity.GroupName = newEntity.GroupName;

            // Синхронное сохранение изменений в базе данных
            ctx.SaveChanges();

            // Возврат успешного результата
            return true;
        }

        /// <summary>
        /// Удаляет существующую группу <see cref="Group"/> из базы данных.
        /// </summary>
        /// <param name="entity">Объект группы для удаления.</param>
        /// <returns>Задача, возвращающая true, если удаление прошло успешно, иначе false, если входной объект null.</returns>
        public override async Task<bool> Remove(Group entity)
        {
            // Проверка на null
            if (entity == null) return false;

            // Удаление группы из контекста базы данных
            ctx.Groups.Remove(entity);
            // Синхронное сохранение изменений
            ctx.SaveChanges();

            // Возврат успешного результата
            return true;
        }
    }

}
