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
        public KindergartnerEditWindowModel(User user, Kindergartner kindergartner, KindergartnerService kindergartnerService)
        {
            Kindergartner = kindergartner;
            _kindergartnerService = kindergartnerService;
            if (kindergartner != null)
            {
                FIO = kindergartner.FIO;
                DateOfBirth = kindergartner.DateOfBirth;
                ParentsContactInfo = kindergartner.ParentsContactInfo;
                
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (kindergartner == null)
                {
                    
                        
                        _kindergartnerService.Add(new Kindergartner() { Kindergartner_Id = Guid.NewGuid(), FIO = this.FIO, DateOfBirth = this.DateOfBirth, ParentsContactInfo = this.ParentsContactInfo });
                    MessageBox.Show($"{this.GetType().Name} - воспитанник добавлен!");

                }
                else
                {
                    _kindergartnerService.Update(kindergartner, new Kindergartner() { Kindergartner_Id = Guid.NewGuid(), FIO = this.FIO, DateOfBirth = this.DateOfBirth, ParentsContactInfo = this.ParentsContactInfo });
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

        public Kindergartner Kindergartner { get => kindergartner; set => Set(ref kindergartner, value, nameof(kindergartner)); }
        public string FIO { get => fio; set => Set(ref fio, value, nameof(fio)); }
        public string DateOfBirth { get => dateOfBirth; set => Set(ref dateOfBirth, value, nameof(dateOfBirth)); }
        public string ParentsContactInfo { get => parentsContactInfo; set => Set(ref parentsContactInfo, value, nameof(parentsContactInfo)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
}
