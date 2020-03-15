using RaspApp.Services;
using RaspApp.Views.Pages;
using Xamarin.Forms;

namespace RaspApp
{
    public partial class App : Application
    {
        public static string AzureBackendUrl = $"https://raspapp.azurewebsites.net/";
        private readonly bool LocalTest = false;
        public static bool LoadTestFacilities = false;
        public static Page StartPage { get; private set; }
        public App()
        {
            InitializeComponent();
            if (LocalTest)
            {
                Controller.Instance = new LocalController();
            }
            else
            {
                Controller.Instance = new HttpController();
            }


            StartPage = MainPage = (new MasterPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
