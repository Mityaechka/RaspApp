
using RaspApp.Models;
using RaspApp.ViewModel;
using RaspApp.Views.Modals;
using Rg.Plugins.Popup.Extensions;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaspApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DayPage : ContentView
    {
        private readonly DayViewModel Day;
        public DayPage(Day day)
        {
            InitializeComponent();
            Day = new DayViewModel(day);
            LessonsListView.ItemsSource = new ObservableCollection<TimeRange>(Day.Model.TimeRanges);
        }
        private async void TimeRangeSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                TimeRange timeRange = LessonsListView.SelectedItem as TimeRange;
                LessonsListView.SelectedItem = null;
                await Navigation.PushPopupAsync(new TimeRangeModal(timeRange));
            }
        }
    }
}