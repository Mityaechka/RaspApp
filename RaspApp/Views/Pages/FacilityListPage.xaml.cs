using RaspApp.Models;
using RaspApp.Services;
using RaspApp.ViewModel;
using RaspApp.Views.Modals;
using Rg.Plugins.Popup.Extensions;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaspApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FacilityListPage : ContentPage
    {
        private FacilityListViewModel Model;
        public FacilityListPage()
        {
            InitializeComponent();
            Load();
        }

        private async void Load()
        {
            await Navigation.PushPopupAsync(new LoadingModal());
            System.Collections.Generic.List<Facility> r = await Controller.Instance.GetFacilities(App.LoadTestFacilities);
            if (r != null)
            {
                Model = new FacilityListViewModel(r);
                BindingContext = this;
                FacilitiesList.ItemsSource = Model.Facilities;
            }
            else
            {
                await Navigation.PopAsync();
            }

            await Navigation.PopPopupAsync();
        }

        private async void FacilitiesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Facility facility = FacilitiesList.SelectedItem as Facility;
                FacilitiesList.SelectedItem = null;
                //await Navigation.PushPopupAsync(new LoadingModal());

                await MasterPage.Instance.Detail.Navigation.PushAsync(new GroupInfoListPage(facility.Index, false, facility));

                //await Navigation.PopPopupAsync();


            }
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new SettingsModal());
        }
    }
}