using RaspApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaspApp.ViewModel
{
    internal class FacilityListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Facility> Facilities
        {
            get => facilities;
            set
            {
                facilities = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Facility> facilities = new ObservableCollection<Facility>();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FacilityListViewModel(ObservableCollection<Facility> facilities)
        {
            Facilities = facilities;
        }
        public FacilityListViewModel(List<Facility> facilities)
        {
            Facilities = new ObservableCollection<Facility>(facilities);
        }

    }
}
