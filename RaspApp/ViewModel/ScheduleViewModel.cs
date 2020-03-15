using RaspApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaspApp.ViewModel
{
    internal class ScheduleViewModel : INotifyPropertyChanged
    {
        private Schedule model;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ScheduleViewModel(Schedule model)
        {
            Model = model;
        }

        public ScheduleViewModel()
        {
        }

        public Schedule Model
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


