using RaspApp.Models;
using RaspApp.ViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace RaspApp.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeRangeModal : PopupPage
    {
        private readonly TimeRangeViewModel Model;
        public TimeRangeModal(TimeRange model)
        {
            InitializeComponent();
            Model = new TimeRangeViewModel(model);
            BindingContext = Model;
        }
    }
}