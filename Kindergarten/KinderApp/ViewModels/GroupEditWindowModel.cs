using KinderApp.Commands;
using KinderData.Entities;
using KinderData.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinderApp.ViewModels
{
    public class GroupEditWindowModel : ViewModelBase
    {
        public GroupEditWindowModel(User user, Group group)
        {
            Group = group;
            if (group != null)
            {
                GroupName = group.GroupName;
                SelectedKindergartner = group.Kindergartners;
            }
            else
            {
                SelectedKindergartner = Kindergartners.FirstOrDefault()!;
            }
            SaveCommand = new RelayCommand(o =>
            {
                if (group == null)
                {
                    GroupService.Add(group);

                }
                else
                {
                    GroupService.Update(group, GroupName, SelectedKindergartner, user);
                }
            });
            CloseCommand = new RelayCommand(o =>
            {
                AppClose();
            });
        }

        private string groupName = string.Empty;
        private Kindergartner selectedKindergartner;

        private Group group;

        public Group Group { get => group; set => Set(ref group, value, nameof(group)); }

        public string GroupName { get => groupName; set => Set(ref groupName, value, nameof(groupName)); }
        public Kindergartner SelectedKindergartner { get => selectedKindergartner; set => Set(ref selectedKindergartner, value, nameof(selectedKindergartner)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
}
