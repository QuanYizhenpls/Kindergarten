using KinderData.Entities;
using KinderDbContext.Abstraction;
using KinderDbContext.Connections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderDbContext.Services
{
    /// <summary>
    /// Сервис для управления сущностями <see cref="Kindergartner"/>.
    /// Наследуется от базового класса <see cref="BaseService{Kindergartner}"/> и реализует основные операции для работы
    /// с базой данных: получение всех сущностей, получение по ID, добавление, обновление и удаление.
    /// </summary>
    public class KindergartnerService : BaseService<Kindergartner>
    {
        /// <summary>
        /// Асинхронно получает список всех объектов <see cref="Kindergartner"/> из базы данных.
        /// </summary>
        /// <returns>
        /// Задача, возвращающая перечисление всех объектов <see cref="Kindergartner"/>.
        /// Может быть пустым, если в базе данных нет ни одного ребёнка.
        /// </returns>
        public override async Task<IEnumerable<Kindergartner?>> GetEntities()
        {
            // Получение списка всех детей из контекста и приведение к IEnumerable<Kindergartner>
            return await Task.FromResult(ctx.Kindergartners.ToList() as IEnumerable<Kindergartner>);
        }

        /// <summary>
        /// Асинхронно получает одного ребёнка из базы данных по уникальному идентификатору <see cref="Guid"/>.
        /// </summary>
        /// <param name="id">Уникальный идентификатор ребёнка.</param>
        /// <returns>
        /// Задача с объектом <see cref="Kindergartner"/>, если объект с заданным идентификатором найден;
        /// иначе возвращает null.
        /// </returns>
        public override async Task<Kindergartner?> GetEntity(Guid id)
        {
            // Поиск ребёнка по идентификатору, возвращается один объект или null, если не найден
            return await Task.FromResult(ctx.Kindergartners.SingleOrDefault(e => e.Kindergartner_Id == id));
        }

        /// <summary>
        /// Асинхронно добавляет нового ребёнка в базу данных.
        /// </summary>
        /// <param name="entity">Объект <see cref="Kindergartner"/> для добавления.</param>
        /// <returns>
        /// Задача, возвращающая true, если добавление прошло успешно;
        /// false, если входящий объект null и добавление не может быть выполнено.
        /// </returns>
        public override async Task<bool> Add(Kindergartner entity)
        {
            if (entity == null) return false;

            // Добавление нового объекта ребёнка в контекст базы данных
            ctx.Kindergartners.Add(entity);

            // Асинхронное сохранение изменений в базе данных
            await ctx.SaveChangesAsync();

            // Возвращаем успешный результат
            return true;
        }

        /// <summary>
        /// Асинхронно обновляет существующую запись ребёнка новыми данными.
        /// </summary>
        /// <param name="entity">Старый объект <see cref="Kindergartner"/>, который необходимо обновить.</param>
        /// <param name="newEntity">Новый объект с обновлёнными данными.</param>
        /// <returns>
        /// Задача, возвращающая true, если обновление прошло успешно;
        /// false, если один из объектов null и обновление невозможно выполнить.
        /// </returns>
        public override async Task<bool> Update(Kindergartner entity, Kindergartner newEntity)
        {
            if (entity == null || newEntity == null) return false;

            // Обновление полей существующего объекта новыми значениями
            entity.FIO = newEntity.FIO;
            entity.DateOfBirth = newEntity.DateOfBirth;
            entity.ParentsContactInfo = newEntity.ParentsContactInfo;
            entity.Group = newEntity.Group;

            // Обновление объекта в контексте базы данных
            ctx.Kindergartners.Update(entity);

            // Сохраняем изменения синхронно (можно заменить на SaveChangesAsync при необходимости)
            ctx.SaveChanges();

            return true;
        }

        /// <summary>
        /// Асинхронно удаляет объект <see cref="Kindergartner"/> из базы данных.
        /// </summary>
        /// <param name="entity">Объект ребёнка для удаления.</param>
        /// <returns>
        /// Задача, возвращающая true, если удаление прошло успешно;
        /// false, если входящий объект null и удаление невозможно выполнить.
        /// </returns>
        public override async Task<bool> Remove(Kindergartner entity)
        {
            if (entity == null) return (false);

            ctx.Kindergartners.Remove(entity);
            ctx.SaveChanges();
            return (true);
        }
    }
}
