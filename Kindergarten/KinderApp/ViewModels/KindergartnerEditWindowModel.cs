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
    /// Модель представления для окна редактирования данных воспитанника детского сада.
    /// Обеспечивает отображение и изменение информации о воспитаннике, а также команды для сохранения и закрытия окна.
    /// </summary>
    public class KindergartnerEditWindowModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр модели с указанными параметрами.
        /// При наличии существующего воспитанника заполняет свойства текущими значениями.
        /// </summary>
        /// <param name="user">Пользователь, инициирующий редактирование (может использоваться для расширения).</param>
        /// <param name="kindergartner">Объект воспитанника для редактирования; если null, создается новый.</param>
        /// <param name="kindergartnerService">Сервис для работы с данными воспитанников (добавление, обновление).</param>
        /// <param name="groups">Список групп, в которых может находиться воспитанник.</param>
        public KindergartnerEditWindowModel(User user, Kindergartner kindergartner, KindergartnerService kindergartnerService, List<Group> groups)
        {
            // Инициализация полей
            Kindergartner = kindergartner;
            _kindergartnerService = kindergartnerService;
            _groups = groups;

            // Если редактируемый воспитанник не null, заполняем свойства текущими данными
            if (kindergartner != null)
            {
                FIO = kindergartner.FIO;
                DateOfBirth = kindergartner.DateOfBirth;
                ParentsContactInfo = kindergartner.ParentsContactInfo;
                SelectedGroup = kindergartner.Group;
            }

            // Команда для сохранения данных воспитанника
            SaveCommand = new RelayCommand(o =>
            {
                // Проверка корректности введенных данных
                if (!ValidateData()) return;

                if (kindergartner == null)
                {
                    // Добавление нового воспитанника через сервис
                    _kindergartnerService.Add(new Kindergartner()
                    {
                        Kindergartner_Id = Guid.NewGuid(),
                        FIO = this.FIO,
                        DateOfBirth = this.DateOfBirth,
                        ParentsContactInfo = this.ParentsContactInfo,
                        GroupId = Guid.NewGuid(),
                        Group = this.SelectedGroup
                    });
                    MessageBox.Show($"{this.GetType().Name} - воспитанник добавлен!");
                }
                else
                {
                    // Обновление существующего воспитанника
                    _kindergartnerService.Update(kindergartner, new Kindergartner()
                    {
                        Kindergartner_Id = Guid.NewGuid(),
                        FIO = this.FIO,
                        DateOfBirth = this.DateOfBirth,
                        ParentsContactInfo = this.ParentsContactInfo,
                        GroupId = Guid.NewGuid(),
                        Group = this.SelectedGroup
                    });
                    MessageBox.Show($"{this.GetType().Name} - воспитанник изменён!");
                }
            });

            // Команда для закрытия окна
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        // Приватные поля для хранения данных и сервисов
        private string fio = string.Empty;
        private string dateOfBirth = string.Empty;
        private string parentsContactInfo = string.Empty;
        private KindergartnerService _kindergartnerService;
        private Kindergartner kindergartner;
        private List<Group> _groups;
        private Group selectedGroup;

        /// <summary>
        /// Объект воспитанника для редактирования/создания.
        /// Свойство оповещает об изменениях.
        /// </summary>
        public Kindergartner Kindergartner { get => kindergartner; set => Set(ref kindergartner, value, nameof(kindergartner)); }

        /// <summary>
        /// Поле для ввода ФИО воспитанника.
        /// Обязательное: показывает сообщение, если пустое.
        /// </summary>
        public string FIO
        {
            get => fio;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                    return;
                }
                Set(ref fio, value, nameof(FIO));
            }
        }

        /// <summary>
        /// Поле для даты рождения воспитанника.
        /// Обязательное: показывает сообщение, если пустое.
        /// </summary>
        public string DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Дата рождения' не может быть пустым.");
                    return;
                }
                Set(ref dateOfBirth, value, nameof(DateOfBirth));
            }
        }

        /// <summary>
        /// Контактная информация родителей воспитанника.
        /// Обязательное поле, при пустой строке выводится сообщение.
        /// </summary>
        public string ParentsContactInfo
        {
            get => parentsContactInfo;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Контакты родителей' не может быть пустым.");
                    return;
                }
                Set(ref parentsContactInfo, value, nameof(ParentsContactInfo));
            }
        }

        /// <summary>
        /// Список групп для выбора воспитанником.
        /// </summary>
        public List<Group> Groups { get => _groups; set => Set(ref _groups, value, nameof(Groups)); }

        /// <summary>
        /// В выбранной группе воспитанник учится.
        /// Обязательное поле: показывает сообщение, если не выбрана группа.
        /// </summary>
        public Group SelectedGroup
        {
            get => selectedGroup;
            set
            {
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать группу.");
                    return;
                }
                Set(ref selectedGroup, value, nameof(SelectedGroup));
            }
        }

        /// <summary>
        /// Команда сохранения изменений воспитанника (добавление или обновление).
        /// </summary>
        public RelayCommand SaveCommand { get; }

        /// <summary>
        /// Команда закрытия окна редактирования.
        /// </summary>
        public RelayCommand CloseCommand { get; }

        /// <summary>
        /// Выполняет проверку корректности введённых данных.
        /// Показано сообщение, если какое-либо обязательное поле не заполнено.
        /// </summary>
        /// <returns>True, если все поля корректны, иначе False.</returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(FIO))
            {
                MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(DateOfBirth))
            {
                MessageBox.Show("Поле 'Дата рождения' не может быть пустым.");
                return false;
            }
            if (string.IsNullOrEmpty(ParentsContactInfo))
            {
                MessageBox.Show("Поле 'Контакты родителей' не может быть пустым.");
                return false;
            }
            if (SelectedGroup == null)
            {
                MessageBox.Show("Необходимо выбрать группу.");
                return false;
            }
            return true;
        }
    }
}
