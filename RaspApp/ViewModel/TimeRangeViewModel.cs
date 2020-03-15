using RaspApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaspApp.ViewModel
{
    internal class TimeRangeViewModel : INotifyPropertyChanged
    {
        private TimeRange model;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TimeRangeViewModel(TimeRange model)
        {
            Model = model;
        }

        public TimeRangeViewModel()
        {
        }

        public TimeRange Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }
    }
}


