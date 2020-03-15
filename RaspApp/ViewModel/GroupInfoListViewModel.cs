using RaspApp.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RaspApp.ViewModel
{
    internal class GroupInfoListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GroupInfo> Groups
        {
            get => groups;
            set
            {
                groups = value;
                OnPropertyChanged();
            }
        }
        public bool IsEmpty => Groups.Count == 0;
        public bool IsLocal { get; set; }

        private ObservableCollection<GroupInfo> groups = new ObservableCollection<GroupInfo>();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GroupInfoListViewModel(ObservableCollection<GroupInfo> Groups)
        {
            this.Groups = Groups;
        }

        public GroupInfoListViewModel()
        {
        }

    }
}
