using RaspApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaspApp.ViewModel
{
    internal class GroupInfoViewModel : INotifyPropertyChanged
    {
        private GroupInfo model;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GroupInfoViewModel(GroupInfo model)
        {
            Model = model;
        }

        public GroupInfoViewModel()
        {
        }

        public GroupInfo Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }
        public static List<GroupInfoViewModel> Convert(List<GroupInfo> groups)
        {
            List<GroupInfoViewModel> models = new List<GroupInfoViewModel>();
            foreach (GroupInfo g in groups)
            {
                models.Add(new GroupInfoViewModel(g));
            }

            return models;
        }
    }
}


