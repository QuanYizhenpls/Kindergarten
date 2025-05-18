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
        public GroupEditWindowModel(User user, Group group, GroupService groupService)
        {
            Group = group;
            _groupService = groupService;
            if (group != null)
            {
                GroupName = group.GroupName;

            }

            SaveCommand = new RelayCommand(o =>
            {
                if (!ValidateData()) return;
                if (group == null)
                {
                    _groupService.Add(new Group() { Group_Id = Guid.NewGuid(), GroupName = this.GroupName });
                    MessageBox.Show($"{this.GetType().Name} - группа добавлена!");

                }
                else
                {
                    _groupService.Update(group, new Group() { Group_Id = Guid.NewGuid(), GroupName = this.GroupName });
                    MessageBox.Show($"{this.GetType().Name} - группа изменена!");
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }


        private string groupName = string.Empty;
        private GroupService _groupService;
        private Group group;

        public Group Group { get => group; set => Set(ref group, value, nameof(group)); }

        public string GroupName { get => groupName; set
            {
                if (string.IsNullOrEmpty(value))
                {
                    MessageBox.Show("Поле 'Название группы' не может быть пустым.");
                    return;
                }
                Set(ref groupName, value, nameof(groupName)); }}

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
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
