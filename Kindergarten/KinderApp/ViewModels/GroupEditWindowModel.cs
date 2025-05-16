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
    public class GroupEditWindowModel : ViewModelBase
    {
        public GroupEditWindowModel(User user, Group group, GroupService groupService, List<Kindergartner> kindergartners)
        {
            Group = group;
            _groupService = groupService;
            _kindergartners = kindergartners;
            if (group != null)
            {
                GroupName = group.GroupName;
                SelectedKindergartner = group.Kindergartner;
               
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (group == null)
                {
                    _groupService.Add(new Group() {Group_Id = Guid.NewGuid(), GroupName = this.GroupName, KindergartnerId = Guid.NewGuid(), Kindergartner = this.SelectedKindergartner});
                    MessageBox.Show($"{this.GetType().Name} - группа добавлена!");

                }
                else
                {
                    _groupService.Update(group, new Group() { Group_Id = Guid.NewGuid(), GroupName = this.GroupName, KindergartnerId = Guid.NewGuid(), Kindergartner = this.SelectedKindergartner });
                    MessageBox.Show($"{this.GetType().Name} - группа изменена!");
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }


        private string groupName = string.Empty;
        private Kindergartner selectedKindergartner;
        private GroupService _groupService;
        private Group group;
        private List<Kindergartner> _kindergartners;

        public Group Group { get => group; set => Set(ref group, value, nameof(group)); }

        public string GroupName { get => groupName; set => Set(ref groupName, value, nameof(groupName)); }
        public Kindergartner SelectedKindergartner { get => selectedKindergartner; set => Set(ref selectedKindergartner, value, nameof(selectedKindergartner)); }
        public List<Kindergartner> Kindergartners { get => _kindergartners; set => Set(ref _kindergartners, value, nameof(_kindergartners)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
       
    }
}
