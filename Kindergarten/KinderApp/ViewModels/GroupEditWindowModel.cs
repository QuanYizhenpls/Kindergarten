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
        public GroupEditWindowModel(User user, Group group, GroupService groupService)
        {
            Group = group;
            if (group != null)
            {
                GroupName = group.GroupName;
                SelectedKindergartner = group.Kindergartners;
                _groupService = groupService;
            }
            
            SaveCommand = new RelayCommand(o =>
            {
                if (group == null)
                {
                    _groupService.Add(group);

                }
                else
                {
                    _groupService.Update(group, new Group());
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

        public Group Group { get => group; set => Set(ref group, value, nameof(group)); }

        public string GroupName { get => groupName; set => Set(ref groupName, value, nameof(groupName)); }
        public Kindergartner SelectedKindergartner { get => selectedKindergartner; set => Set(ref selectedKindergartner, value, nameof(selectedKindergartner)); }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }
    }
}
