using RaspApp.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace RaspApp.ViewModel
{
    internal class FacilityViewModel : INotifyPropertyChanged
    {
        private Facility model;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FacilityViewModel(Facility model)
        {
            Model = model;
        }

        public Facility Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged();
            }
        }

        [Obsolete]
        public ICommand ClickCommand => new Command<string>((url) =>
        {
            Device.OpenUri(new Uri(url));
        });
    }
}


