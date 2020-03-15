using RaspApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaspApp.ViewModel
{
    internal class DayViewModel : INotifyPropertyChanged
    {
        private Day model;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DayViewModel(Day model)
        {
            Model = model;
        }

        public DayViewModel()
        {
        }

        public Day Model
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


