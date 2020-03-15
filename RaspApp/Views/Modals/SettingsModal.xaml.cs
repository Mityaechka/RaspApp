using RaspApp.ViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace RaspApp.Views.Modals
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsModal : PopupPage
    {
        private readonly SettingsViewModel settings;
        public SettingsModal()
        {
            InitializeComponent();

            settings = new SettingsViewModel();
            BindingContext = settings;
        }

        protected override bool OnBackButtonPressed()
        {
            settings.Save();
            return base.OnBackButtonPressed();

        }

        protected override bool OnBackgroundClicked()
        {
            settings.Save();
            return base.OnBackgroundClicked();
        }
    }
}