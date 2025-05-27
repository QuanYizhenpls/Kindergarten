using KinderApp.Commands;
using KinderData.Entities;
using KinderApp.VIews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;
using System.Security.AccessControl;
using KinderDbContext.Services;
using KinderDbContext.Connections;

namespace KinderApp.ViewModels
{
    /// <summary>
    /// Модель представления для главного меню приложения.
    /// Обеспечивает управление командами и данными, отображаемыми в главном окне.
    /// </summary>
    public class MenuWindowModel : ViewModelBase
    {
        /// <summary>
        /// Инициализация новой модели представления с передачей необходимых сервисов и текущего пользователя.
        /// Загружает первоначальные данные и устанавливает команды для работы.
        /// </summary>
        /// <param name="user">Объект текущего пользователя, использующего приложение.</param>
        /// <param name="agreementsService">Сервис для работы с договорами (agreements).</param>
        /// <param name="employeeDataService">Сервис для работы с данными сотрудников.</param>
        /// <param name="employeeService">Сервис для работы с сотрудниками.</param>
        /// <param name="groupService">Сервис для работы с группами.</param>
        /// <param name="kindergartnerService">Сервис для работы с воспитанниками детского сада.</param>
        /// <param name="planService">Сервис для работы с учебными планами.</param>
        /// <param name="salaryService">Сервис для работы с начислением заработной платы.</param>
        public MenuWindowModel(
            User user,
            AgreementsService agreementsService,
            EmployeeDataService employeeDataService,
            EmployeeService employeeService,
            GroupService groupService,
            KindergartnerService kindergartnerService,
            PlanService planService,
            SalaryService salaryService)
        {
            /// <summary>
            /// Указание текущего пользователя.
            /// </summary>
            CurrentUser = user;

            /// <summary>
            /// Инициализация приватных полей сервисов для дальнейшего использования.
            /// </summary>
            _agreementService = agreementsService;
            _employeeDataService = employeeDataService;
            _employeeService = employeeService;
            _kindergartnerService = kindergartnerService;
            _groupService = groupService;
            _planService = planService;
            _salaryService = salaryService;

            // Обновление списков данных, отображаемых в интерфейсе.
            UpdateLists();

            /// <summary>
            /// Команда для добавления нового соглашения (договора).
            /// Формирует диалоговое окно для ввода договора и обновляет списки.
            /// </summary>
            AAddCommand = new RelayCommand(o =>
            {
                // Открытие окна редактирования соглашения
                OpenWindowDialog(new AgreementsEditWindow(CurrentUser, employees: Employees));
                // Обновление отображаемых данных после добавления
                UpdateLists();
            });

            /// <summary>
            /// Команда для обновления выбранного соглашения.
            /// Проверяет, выбран ли договор, и вызывает диалог на редактирование.
            /// В случае ошибки выводит сообщение.
            /// </summary>
            AUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddAgreement != null)
                {
                    try
                    {
                        OpenWindowDialog(new AgreementsEditWindow(CurrentUser, SelectedAddAgreement, employees: Employees));
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для удаления выбранного соглашения.
            /// Проверяет наличие выбранного договора, вызывает его удаление через сервис и обновляет список.
            /// В случае ошибок выводит сообщение.
            /// </summary>
            ARemoveCommand = new RelayCommand(o =>
            {
                if (SelectedAddAgreement != null)
                {
                    try
                    {
                        _agreementService.Remove(SelectedAddAgreement).GetAwaiter();
                        UpdateLists();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            // Команды работы с Employee
            /// <summary>
            /// Команда для открытия окна для добавления нового сотрудника.
            /// Создает и отображает диалоговое окно для редактирования информации о новом сотруднике,
            /// а затем обновляет списки данных в интерфейсе.
            /// </summary>
            EAddCommand = new RelayCommand(o =>
            {
                // Открытие окна редактирования сотрудника для добавления нового
                OpenWindowDialog(new EmployeeEditWindow(CurrentUser, groups: Groups));
                // Обновление списка сотрудников после добавления
                UpdateLists();
            });

            /// <summary>
            /// Команда для редактирования выбранного сотрудника.
            /// Проверяет, выбран ли сотрудник, и вызывает диалоговое окно для редактирования его данных.
            /// В случае ошибок выводит сообщения.
            /// </summary>
            EUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddEmployee != null)
                {
                    try
                    {
                        // Открытие окна редактирования выбранного сотрудника
                        OpenWindowDialog(new EmployeeEditWindow(CurrentUser, SelectedAddEmployee, groups: Groups));
                        // Обновление списков после редактирования
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        // Логирование ошибки в отладочную консоль
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        // Показ сообщения об ошибке пользователю
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    // Сообщение, если сотрудник не выбран для редактирования
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для удаления выбранного сотрудника.
            /// Проверяет, выбран ли сотрудник, вызывает его удаление через сервис и обновляет список.
            /// В случае ошибок выводит сообщение.
            /// </summary>
            ERemoveCommand = new RelayCommand(o =>
            {
                if (SelectedAddEmployee != null)
                {
                    try
                    {
                        _employeeService.Remove(SelectedAddEmployee).GetAwaiter();
                        UpdateLists();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            // Команды работы с EmployeeData
            /// <summary>
            /// Команда для открытия окна для добавления новых данных о сотруднике (например, личных данных).
            /// Создает диалог для редактирования дополнительных данных сотрудника и обновляет списки.
            /// </summary>
            EDAddCommand = new RelayCommand(o =>
            {
                OpenWindowDialog(new EmployeeDataEditWindow(CurrentUser, employees: Employees));
                UpdateLists();
            });

            /// <summary>
            /// Команда для редактирования выбранных данных по сотруднику.
            /// Проверяет наличие выбранных данных, вызывает диалог для редактирования, и обновляет списки.
            /// В случае ошибок отображает сообщение.
            /// </summary>
            EDUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddEmployeeData != null)
                {
                    try
                    {
                        OpenWindowDialog(new EmployeeDataEditWindow(CurrentUser, SelectedAddEmployeeData, employees: Employees));
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для удаления выбранных данных о сотруднике.
            /// Проверяет, выбран ли элемент, вызывает его удаление сервисом и обновляет список.
            /// В случае ошибок выводит сообщение.
            /// </summary>
            EDRemoveCommand = new RelayCommand(o =>
            {
                if (SelectedAddEmployeeData != null)
                {
                    try
                    {

                        _employeeDataService.Remove(SelectedAddEmployeeData).GetAwaiter();
                        UpdateLists();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            // Команды работы с Group
            /// <summary>
            /// Команда для открытия окна для добавления новой группы.
            /// Создает диалоговое окно для редактирования группы и обновляет отображаемые списки.
            /// </summary>
            GAddCommand = new RelayCommand(o =>
            {
                // Открытие окна редактирования группы для добавления новой
                OpenWindowDialog(new GroupEditWindow(CurrentUser));
                // Обновление списков после добавления
                UpdateLists();
            });

            /// <summary>
            /// Команда для редактирования выбранной группы.
            /// Проверяет, выбран ли элемент, и вызывает окно редактирования.
            /// В случае ошибок выводит сообщение.
            ///
            /// </summary>
            GUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddGroup != null)
                {
                    try
                    {
                        // Открытие окна редактирования выбранной группы
                        OpenWindowDialog(new GroupEditWindow(CurrentUser, SelectedAddGroup));
                        // Обновление списков после редактирования
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        // Логирование ошибки
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        // Вывод сообщения пользователю
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    // Сообщение, если группа не выбрана для редактирования
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для удаления выбранной группы.
            /// Проверяет, выбран ли элемент, вызывает удаление через сервис и обновляет список.
            /// В случае ошибок выводит сообщение.
            ///
            /// </summary>
            GRemoveCommand = new RelayCommand(o =>
            {
                if (SelectedAddGroup != null)
                {
                    try
                    {
                        // Удаление выбранной группы через сервис
                        _groupService.Remove(SelectedAddGroup).GetAwaiter();
                        // Обновление списка после удаления
                        UpdateLists();

                    }
                    catch (Exception ex)
                    {
                        // Логирование ошибки
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        // Вывод сообщения пользователю
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    // Сообщение, если группа не выбрана для удаления
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команды для работы с воспитанниками (Kindergartner).
            /// </summary>

            /// <summary>
            /// Команда для открытия окна добавления нового воспитанника.
            /// Создает диалоговое окно для редактирования и добавления воспитанника и обновляет списки.
            /// </summary>
            KAddCommand = new RelayCommand(o =>
            {
                // Открытие окна редактирования воспитанника для добавления
                OpenWindowDialog(new KindergartnerEditWindow(CurrentUser, groups: Groups));
                // Обновление данных после добавления
                UpdateLists();
            });

            /// <summary>
            /// Команда для редактирования выбранного воспитанника.
            /// Проверяет наличие выбранного элемента, вызывает диалог для редактирования, и обновляет списки.
            /// В случае ошибок выводит сообщение.
            /// </summary>
            KUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddKindergartner != null)
                {
                    try
                    {
                        OpenWindowDialog(new KindergartnerEditWindow(CurrentUser, SelectedAddKindergartner, groups: Groups));
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });
            /// <summary>
            /// Команда для удаления выбранного воспитанника.
            /// Проверяет, выбран ли элемент, вызывает удаление через сервис и обновляет список.
            /// В случае ошибок выводит сообщение.
            /// </summary>
            KRemoveCommand = new RelayCommand(o =>
            {
                if (SelectedAddKindergartner != null)
                {
                    try
                    {
                        _kindergartnerService.Remove(SelectedAddKindergartner).GetAwaiter();
                        UpdateLists();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });
            // Команды работы с Plan
            /// <summary>
            /// Команда для открытия окна добавления нового плана.
            /// Создает диалоговое окно для ввода информации о новом плане с передачей списка сотрудников.
            /// После закрытия окна обновляет отображаемые данные.
            /// </summary>
            PAddCommand = new RelayCommand(o =>
            {
                // Открытие окна редактирования плана с передачей текущего пользователя и списка сотрудников
                OpenWindowDialog(new PlanEditWindow(CurrentUser, employees: Employees));
                // Обновление списков после добавления нового плана
                UpdateLists();
            });

            /// <summary>
            /// Команда для редактирования выбранного плана.
            /// Проверяет, указан ли выбранный план для редактирования.
            /// Если да — открывает окно редактирования с передачей выбранного плана и сотрудников.
            /// При ошибках отображает сообщения для пользователя.
            /// В случае отсутствия выбранного объекта — выводит предупреждение.
            /// </summary>
            PUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddPlan != null)
                {
                    try
                    {
                        // Открытие окна редактирования выбранного плана с передачей данных
                        OpenWindowDialog(new PlanEditWindow(CurrentUser, SelectedAddPlan, employees: Employees));
                        // Обновление списка после редактирования
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        // Вывод подробностей ошибки в отладочную консоль
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        // Информирование пользователя о возникшей ошибке
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    // Сообщение о попытке редактирования без выбора объекта
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для удаления выбранного плана.
            /// Проверяет, выбран ли план, и если да, вызывает удаление через сервис.
            /// Обновляет списки после удаления.
            /// При ошибках выводит сообщения в интерфейс.
            /// При отсутствии выбора объекта — сообщает пользователю.
            /// </summary>
            PRemoveCommand = new RelayCommand(o =>
            {
                if (SelectedAddPlan != null)
                {
                    try
                    {
                        // Запуск асинхронного удаления выбранного плана, ожидание завершения
                        _planService.Remove(SelectedAddPlan).GetAwaiter();
                        // Обновление списков после удаления
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        // Логирование исключения в консоль отладки
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        // Отображение сообщения пользователю
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    // Предупреждение о попытке удаления без выбора
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для открытия окна добавления новой записи по зарплате.
            /// Создает диалог для указания данных о зарплате с передачей списка сотрудников.
            /// Обновляет данные после закрытия окна.
            /// </summary>
            SAddCommand = new RelayCommand(o =>
            {
                // Открытие окна редактирования зарплаты
                OpenWindowDialog(new SalaryEditWindow(CurrentUser, employees: Employees));
                // Обновление списков после добавления
                UpdateLists();
            });

            /// <summary>
            /// Команда для редактирования выбранной записи по зарплате.
            /// Проверяет, выбран ли элемент, открывает окно редактирования.
            /// В случае исключений информирует пользователя.
            /// Если ничего не выбрано — выводит предупреждение.
            /// </summary>
            SUpdateCommand = new RelayCommand(o =>
            {
                if (SelectedAddSalary != null)
                {
                    try
                    {
                        OpenWindowDialog(new SalaryEditWindow(CurrentUser, SelectedAddSalary, employees: Employees));
                        UpdateLists();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка редактировать задачу не выбрав её!");
                }
            });

            /// <summary>
            /// Команда для удаления выбранной записи по зарплате.
            /// Проверяет наличие выбранного объекта, вызывает удаление через сервис и обновляет списки.
            /// При ошибках отображает информацию пользователю.
            /// Если объект не выбран — предупреждает.
            /// </summary>
            SRemoveCommand = new RelayCommand(o =>
            {
                if (SelectedAddSalary != null)
                {
                    try
                    {
                        _salaryService.Remove(SelectedAddSalary).GetAwaiter();
                        UpdateLists();

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.GetType()} - {ex.Message}");
                        MessageBox.Show($"{ex.GetType()} - {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"{this.GetType().Name} - попытка удалить задачу не выбрав её!");
                }
            });
        }

        /// <summary>
        /// Метод обновления данных списков всех основных сущностей.
        /// Запрашивает данные с помощью сервисов для каждой сущности и обновляет соответствующие списки,
        /// используемые в интерфейсе и логике приложения.
        /// </summary>
        void UpdateLists()
        {
            // Получение и обновление списка договоров (Agreements) из сервиса
            Agreements = _agreementService.GetEntities().Result.ToList();

            // Получение и обновление списка сотрудников (Employees)
            Employees = _employeeService.GetEntities().Result.ToList();

            // Получение и обновление списка дополнительных данных сотрудников (EmployeeDatas)
            EmployeeDatas = _employeeDataService.GetEntities().Result.ToList();

            // Получение и обновление списка групп (Groups)
            Groups = _groupService.GetEntities().Result.ToList();

            // Получение и обновление списка воспитанников (Kindergartners)
            Kindergartners = _kindergartnerService.GetEntities().Result.ToList();

            // Получение и обновление списка планов (Plans)
            Plans = _planService.GetEntities().Result.ToList();

            // Получение и обновление списка зарплат (Salaries)
            Salaries = _salaryService.GetEntities().Result.ToList();
        }

        //поля
        /// <summary>
        /// Текущий пользователь, под которым работает приложение или система.
        /// </summary>
        private User currentUser;

        /// <summary>
        /// Выбранный договор для операций редактирования, удаления и т.д.
        /// </summary>
        private Agreement selectedAddAgreement;

        /// <summary>
        /// Выбранный сотрудник для операций редактирования, удаления и т.д.
        /// </summary>
        private Employee selectedAddEmployee;

        /// <summary>
        /// Выбранные дополнительные данные сотрудника.
        /// </summary>
        private EmployeeData selectedAddEmployeeData;

        /// <summary>
        /// Выбранная группа.
        /// </summary>
        private Group selectedAddGroup;

        /// <summary>
        /// Выбранный воспитанник.
        /// </summary>
        private Kindergartner selectedAddKindergartner;

        /// <summary>
        /// Выбранный план.
        /// </summary>
        private Plan selectedAddPlan;

        /// <summary>
        /// Выбранная запись о зарплате.
        /// </summary>
        private Salary selectedAddSalary;

        /// <summary>
        /// Сервис для работы с договорами.
        /// </summary>
        private AgreementsService _agreementService;

        /// <summary>
        /// Сервис для работы с сотрудниками.
        /// </summary>
        private EmployeeService _employeeService;

        /// <summary>
        /// Сервис для работы с дополнительными данными сотрудников.
        /// </summary>
        private EmployeeDataService _employeeDataService;

        /// <summary>
        /// Сервис для работы с группами.
        /// </summary>
        private GroupService _groupService;

        /// <summary>
        /// Сервис для работы с воспитанниками.
        /// </summary>
        private KindergartnerService _kindergartnerService;

        /// <summary>
        /// Сервис для работы с планами.
        /// </summary>
        private PlanService _planService;

        /// <summary>
        /// Сервис для работы с зарплатами.
        /// </summary>
        private SalaryService _salaryService;

        /// <summary>
        /// Список договоров, загруженных из базы или другого хранилища.
        /// </summary>
        private List<Agreement> _agreements;

        /// <summary>
        /// Список сотрудников.
        /// </summary>
        private List<Employee> _employees;

        /// <summary>
        /// Список дополнительных данных о сотрудниках.
        /// </summary>
        private List<EmployeeData> _employeeDatas;

        /// <summary>
        /// Список групп.
        /// </summary>
        private List<Group> _groups;

        /// <summary>
        /// Список воспитанников.
        /// </summary>
        private List<Kindergartner> _kindergartners;

        /// <summary>
        /// Список планов.
        /// </summary>
        private List<Plan> _plans;

        /// <summary>
        /// Список записей о зарплатах.
        /// </summary>
        private List<Salary> _salaries;

        /// <summary>
        /// Текущий пользователь с уведомлением об изменении.
        /// </summary>
        public User CurrentUser
        {
            get => currentUser;
            set => Set(ref currentUser, value, nameof(User));
        }
        /// <summary>
        /// Выбранный договор с уведомлением об изменении.
        /// </summary>
        public Agreement SelectedAddAgreement
        {
            get => selectedAddAgreement;
            set => Set(ref selectedAddAgreement, value, nameof(selectedAddAgreement));
        }

        /// <summary>
        /// Выбранный сотрудник с уведомлением об изменении.
        /// </summary>
        public Employee SelectedAddEmployee
        {
            get => selectedAddEmployee;
            set => Set(ref selectedAddEmployee, value, nameof(selectedAddEmployee));
        }

        /// <summary>
        /// Выбранные дополнительные данные сотрудника с уведомлением об изменении.
        /// </summary>
        public EmployeeData SelectedAddEmployeeData
        {
            get => selectedAddEmployeeData;
            set => Set(ref selectedAddEmployeeData, value, nameof(selectedAddEmployeeData));
        }

        /// <summary>
        /// Выбранная группа с уведомлением об изменении.
        /// </summary>
        public Group SelectedAddGroup
        {
            get => selectedAddGroup;
            set => Set(ref selectedAddGroup, value, nameof(selectedAddGroup));
        }

        /// <summary>
        /// Выбранный воспитанник с уведомлением об изменении.
        /// </summary>
        public Kindergartner SelectedAddKindergartner
        {
            get => selectedAddKindergartner;
            set => Set(ref selectedAddKindergartner, value, nameof(selectedAddKindergartner));
        }

        /// <summary>
        /// Выбранный план с уведомлением об изменении.
        /// </summary>
        public Plan SelectedAddPlan
        {
            get => selectedAddPlan;
            set => Set(ref selectedAddPlan, value, nameof(selectedAddPlan));
        }

        /// <summary>
        /// Выбранная запись о зарплате с уведомлением об изменении.
        /// </summary>
        public Salary SelectedAddSalary
        {
            get => selectedAddSalary;
            set => Set(ref selectedAddSalary, value, nameof(selectedAddSalary));
        }

        /// <summary>
        /// Список договоров с уведомлением об изменении.
        /// </summary>
        public List<Agreement> Agreements
        {
            get => _agreements;
            set => Set(ref _agreements, value, nameof(Agreements));
        }

        /// <summary>
        /// Список сотрудников с уведомлением об изменении.
        /// </summary>
        public List<Employee> Employees
        {
            get => _employees;
            set => Set(ref _employees, value, nameof(Employees));
        }

        /// <summary>
        /// Список дополнительных данных сотрудников с уведомлением об изменении.
        /// </summary>
        public List<EmployeeData> EmployeeDatas
        {
            get => _employeeDatas;
            set => Set(ref _employeeDatas, value, nameof(EmployeeDatas));
        }

        /// <summary>
        /// Список групп с уведомлением об изменении.
        /// </summary>
        public List<Group> Groups
        {
            get => _groups;
            set => Set(ref _groups, value, nameof(Groups));
        }

        /// <summary>
        /// Список планов с уведомлением об изменении.
        /// </summary>
        public List<Plan> Plans
        {
            get => _plans;
            set => Set(ref _plans, value, nameof(Plans));
        }

        /// <summary>
        /// Список зарплат с уведомлением об изменении.
        /// </summary>
        public List<Salary> Salaries
        {
            get => _salaries;
            set => Set(ref _salaries, value, nameof(Salaries));
        }

        /// <summary>
        /// Список воспитанников с уведомлением об изменении.
        /// </summary>
        public List<Kindergartner> Kindergartners
        {
            get => _kindergartners;
            set => Set(ref _kindergartners, value, nameof(Kindergartners));
        }

        //Команды
        /// <summary>
        /// Команда на добавление нового договора (Agreement).
        /// Используется для создания и открытия интерфейса добавления договора.
        /// </summary>
        public RelayCommand AAddCommand { get; }

        /// <summary>
        /// Команда на обновление (редактирование) выбранного договора.
        /// Позволяет открыть интерфейс редактирования и сохранить изменения.
        /// </summary>
        public RelayCommand AUpdateCommand { get; }

        /// <summary>
        /// Команда на удаление выбранного договора.
        /// Отвечает за удаление данных договора из системы.
        /// </summary>
        public RelayCommand ARemoveCommand { get; }

        /// <summary>
        /// Команда на добавление нового сотрудника (Employee).
        /// Используется для создания записи сотрудника через UI.
        /// </summary>
        public RelayCommand EAddCommand { get; }

        /// <summary>
        /// Команда на обновление (редактирование) выбранного сотрудника.
        /// Позволяет изменить данные сотрудника.
        /// </summary>
        public RelayCommand EUpdateCommand { get; }

        /// <summary>
        /// Команда на удаление выбранного сотрудника.
        /// Отвечает за удаление данных сотрудника из базы.
        /// </summary>
        public RelayCommand ERemoveCommand { get; }

        /// <summary>
        /// Команда на добавление новых данных сотрудника (EmployeeData).
        /// Используется для ввода и сохранения дополнительных данных сотрудника.
        /// </summary>
        public RelayCommand EDAddCommand { get; }

        /// <summary>
        /// Команда на обновление выбранных данных сотрудника.
        /// Позволяет редактировать дополнительную информацию о сотруднике.
        /// </summary>
        public RelayCommand EDUpdateCommand { get; }

        /// <summary>
        /// Команда на удаление выбранных данных сотрудника.
        /// Отвечает за удаление дополнительных данных сотрудника.
        /// </summary>
        public RelayCommand EDRemoveCommand { get; }

        /// <summary>
        /// Команда на добавление новой группы (Group).
        /// Открывает интерфейс для создания группы.
        /// </summary>
        public RelayCommand GAddCommand { get; }

        /// <summary>
        /// Команда на обновление выбранной группы.
        /// Позволяет изменить информацию о группе.
        /// </summary>
        public RelayCommand GUpdateCommand { get; }

        /// <summary>
        /// Команда на удаление выбранной группы.
        /// Удаляет группу из системы.
        /// </summary>
        public RelayCommand GRemoveCommand { get; }

        /// <summary>
        /// Команда на добавление нового воспитанника (Kindergartner).
        /// Используется для создания записи о воспитаннике.
        /// </summary>
        public RelayCommand KAddCommand { get; }

        /// <summary>
        /// Команда на обновление информации о выбранном воспитаннике.
        /// Позволяет редактировать данные воспитанника.
        /// </summary>
        public RelayCommand KUpdateCommand { get; }

        /// <summary>
        /// Команда на удаление выбранного воспитанника.
        /// Удаляет запись воспитанника из системы.
        /// </summary>
        public RelayCommand KRemoveCommand { get; }

        /// <summary>
        /// Команда на добавление нового плана (Plan).
        /// Открывает окно для создания новой записи плана.
        /// </summary>
        public RelayCommand PAddCommand { get; }

        /// <summary>
        /// Команда на обновление выбранного плана.
        /// Позволяет редактировать данные выбранного плана.
        /// </summary>
        public RelayCommand PUpdateCommand { get; }

        /// <summary>
        /// Команда на удаление выбранного плана.
        /// Удаляет план из базы данных.
        /// </summary>
        public RelayCommand PRemoveCommand { get; }

        /// <summary>
        /// Команда на добавление новой записи зарплаты (Salary).
        /// Используется для создания информации по зарплате сотрудника.
        /// </summary>
        public RelayCommand SAddCommand { get; }

        /// <summary>
        /// Команда на обновление выбранной записи зарплаты.
        /// Позволяет редактировать данные о зарплате.
        /// </summary>
        public RelayCommand SUpdateCommand { get; }

        /// <summary>
        /// Команда на удаление выбранной записи зарплаты.
        /// </summary>
        public RelayCommand SRemoveCommand { get; }
       
    }
}
