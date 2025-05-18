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
    public class KindergartnerEditWindowModel: ViewModelBase
    {
        public KindergartnerEditWindowModel(User user, Kindergartner kindergartner, KindergartnerService kindergartnerService, List<Group> groups)
        {
            Kindergartner = kindergartner;
            _kindergartnerService = kindergartnerService;
            _groups = groups;
            if (kindergartner != null)
            {
                FIO = kindergartner.FIO;
                DateOfBirth = kindergartner.DateOfBirth;
                ParentsContactInfo = kindergartner.ParentsContactInfo;
                SelectedGroup = kindergartner.Group;
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;
                if (kindergartner == null)
                {
                        
                        _kindergartnerService.Add(new Kindergartner() { Kindergartner_Id = Guid.NewGuid(), FIO = this.FIO, DateOfBirth = this.DateOfBirth, ParentsContactInfo = this.ParentsContactInfo, GroupId = Guid.NewGuid(), Group = this.SelectedGroup});
                    MessageBox.Show($"{this.GetType().Name} - воспитанник добавлен!");

                }
                else
                {
                    _kindergartnerService.Update(kindergartner, new Kindergartner() { Kindergartner_Id = Guid.NewGuid(), FIO = this.FIO, DateOfBirth = this.DateOfBirth, ParentsContactInfo = this.ParentsContactInfo, GroupId = Guid.NewGuid(), Group = this.SelectedGroup });
                    MessageBox.Show($"{this.GetType().Name} - воспитанник изменён!");
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        private string fio = string.Empty;
        private string dateOfBirth = string.Empty;
        private string parentsContactInfo = string.Empty;
        private KindergartnerService _kindergartnerService;
        private Kindergartner kindergartner;
        private List<Group> _groups;
        private Group selectedGroup;

        public Kindergartner Kindergartner { get => kindergartner; set => Set(ref kindergartner, value, nameof(kindergartner)); }
        public string FIO
        {
            get => fio; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'ФИО' не может быть пустым.");
                    return;
                }
                Set(ref fio, value, nameof(fio));
            }
        }
        public string DateOfBirth { get => dateOfBirth; set {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Дата рождения' не может быть пустым.");
                    return;
                }
                Set(ref dateOfBirth, value, nameof(dateOfBirth)); } }
        public string ParentsContactInfo { get => parentsContactInfo; set {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Контакты родителей' не может быть пустым.");
                    return;
                }
                Set(ref parentsContactInfo, value, nameof(parentsContactInfo)); } }
        public List<Group> Groups { get => _groups; set => Set(ref _groups, value, nameof(Groups)); }
        public Group SelectedGroup
        {
            get => selectedGroup; set
            {
                if (value == null)
                {
                    MessageBox.Show("Необходимо выбрать группу.");
                    return;
                }
                Set(ref selectedGroup, value, nameof(selectedGroup));
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
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
