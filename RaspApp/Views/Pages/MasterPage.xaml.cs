using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaspApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public static MasterPage Instance { get; private set; }
        public bool IsDetailSet { get; set; } = false;
        public MasterPage()
        {
            InitializeComponent();
            Instance = this;
            //Detail =new NavigationPage(new GroupInfoListPage(true));
            Detail = new NavigationPage(new GroupInfoListPage(true));
        }

        private void ScheduleOpen(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail.Navigation.PushAsync(new FacilityListPage());
        }
        private void LoaclScheduleOpen(object sender, EventArgs e)
        {
            IsPresented = false;
            Detail = new NavigationPage(new GroupInfoListPage(true));
        }
    }
}