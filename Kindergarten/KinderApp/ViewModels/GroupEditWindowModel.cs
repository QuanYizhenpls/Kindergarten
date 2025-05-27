using KinderApp.Commands;
using KinderData.Entities;
using KinderDbContext.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinderApp.ViewModels
{
    /// <summary>
    /// Модель представления для окна редактирования данных группы.
    /// Обеспечивает взаимодействие с данными группы, включая редактирование названия, 
    /// а также команды для сохранения изменений и закрытия окна.
    /// </summary>
    public class GroupEditWindowModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="GroupEditWindowModel"/> с указанными параметрами.
        /// При наличии существующей группы предварительно заполняет свойства текущими значениями.
        /// </summary>
        /// <param name="user">Текущий пользователь (не используется, но может быть применен для расширения).</param>
        /// <param name="group">Редактируемая группа; если null, создается новая группа.</param>
        /// <param name="groupService">Сервис для управления группами (добавление, обновление).</param>
        public GroupEditWindowModel(User user, Group group, GroupService groupService)
        {
            Group = group;
            _groupService = groupService;

            // Если редактируемая группа не null, инициализируем имя группы из модели
            if (group != null)
            {
                GroupName = group.GroupName;
            }

            // Команда сохранения данных группы
            SaveCommand = new RelayCommand(o =>
            {
                // Проверяем валидность введенных данных перед сохранением
                if (!ValidateData()) return;

                if (group == null)
                {
                    // Добавляем новую группу через сервис
                    _groupService.Add(new Group()
                    {
                        Group_Id = Guid.NewGuid(),
                        GroupName = this.GroupName
                    });
                    MessageBox.Show($"{this.GetType().Name} - группа добавлена!");
                }
                else
                {
                    // Обновляем существующую группу
                    _groupService.Update(group, new Group()
                    {
                        Group_Id = Guid.NewGuid(),
                        GroupName = this.GroupName
                    });
                    MessageBox.Show($"{this.GetType().Name} - группа изменена!");
                }
            });

            // Команда закрытия окна редактирования
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        // Приватное поле для хранения названия группы с инкапсуляцией
        private string groupName = string.Empty;
        // Приватное поле для хранения сервиса работы с группами
        private GroupService _groupService;
        // Приватное поле для хранения текущего объекта группы
        private Group group;

        /// <summary>
        /// Текущая группа, которая редактируется.
        /// Обеспечивает связь с моделью данных и уведомление об изменениях.
        /// </summary>
        public Group Group
        {
            get => group;
            set => Set(ref group, value, nameof(Group));
        }

        /// <summary>
        /// Название группы.
        /// Обязательное поле: при попытке установить пустое значение выводится сообщение об ошибке.
        /// </summary>
        public string GroupName
        {
            get => groupName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Название группы' не может быть пустым.");
                    return;
                }
                Set(ref groupName, value, nameof(GroupName));
            }
        }

        /// <summary>
        /// Команда сохранения группы — добавляет новую группу или обновляет существующую.
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда закрытия окна редактирования группы.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// /// Выполняет проверку валидности данных перед сохранением.
        /// Проверяет, что поле <see cref="GroupName"/> заполнено.
        /// </summary>
        /// <returns>True, если данные корректны, иначе false.</returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(GroupName))
            {
                MessageBox.Show("Поле 'Название группы' не может быть пустым.");
                return false;
            }
            return true;
        }
    }
}
