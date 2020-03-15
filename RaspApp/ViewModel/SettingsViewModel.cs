using Newtonsoft.Json;
using Plugin.Settings;
using RaspApp.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaspApp.ViewModel
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        private SettingsModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SettingsViewModel()
        {
            Model = JsonConvert.DeserializeObject<SettingsModel>(
                CrossSettings.Current.GetValueOrDefault("Settings", ""));
            if (Model == null)
            {
                Model = new SettingsModel();
            }
        }

        public SettingsModel Model
        {
            get => model;
            set
            {
                model = value;
                OnPropertyChanged();

            }
        }
        public void Save()
        {
            CrossSettings.Current.AddOrUpdateValue("Settings", JsonConvert.SerializeObject(Model));
        }
    }
}


