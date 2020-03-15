using RaspApp.Models;
using RaspApp.ViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace RaspApp.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacilityModal : PopupPage
    {
        private readonly FacilityViewModel Facility;
        public FacilityModal(Facility facility)
        {
            InitializeComponent();

            Facility = new FacilityViewModel(facility);
            BindingContext = Facility;
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();

        }

    }
}