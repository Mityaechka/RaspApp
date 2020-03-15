using Newtonsoft.Json;
using Plugin.Settings;
using RaspApp.Models;
using RaspApp.Services;
using RaspApp.ViewModel;
using RaspApp.Views.Modals;
using RaspApp.Views.Pages;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaspApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupInfoListPage : ContentPage
    {
        private List<Schedule> Schedules;
        private GroupInfoListViewModel model;
        private bool isFirstLod = true;
        private readonly int facilityIndex = -1;
        private readonly Facility facility;
        public GroupInfoListPage(bool LoadLocal)
        {
            InitializeComponent();
            OnLoaded(LoadLocal);
        }
        public GroupInfoListPage(int index, bool LoadLocal, Facility facility)
        {
            InitializeComponent();
            this.facility = facility;
            facilityIndex = index;
            OnLoaded(LoadLocal, false, index);

        }

        public async void OnLoaded(bool LoadLocal, bool Refresh = false, int index = -1)
        {
            GroupInfoList.IsRefreshing = false;
            if (!Refresh)
            {
                await Navigation.PushPopupAsync(new LoadingModal());
            }

            model = new GroupInfoListViewModel();
            if (!LoadLocal)
            {
                if (index != -1)
                {
                    List<GroupInfo> groups = await Controller.Instance.GetShedulesList(index);
                    if (groups != null)
                    {
                        model.Groups = new ObservableCollection<GroupInfo>(groups);
                        Title = facility.Name;
                    }
                    else
                    {
                        await Navigation.PopAsync();
                    }
                }
            }
            else
            {

                List<Schedule> s = JsonConvert.DeserializeObject<List<Schedule>>(
            CrossSettings.Current.GetValueOrDefault("Saved", ""));
                Schedules = s;
                if (s == null)
                {
                    s = new List<Schedule>();
                }

                model.Groups = new ObservableCollection<GroupInfo>(
                        s.Select<Schedule, GroupInfo>(x => x.Group).ToList());
                Title = "Мое расписание";
            }
            model.IsLocal = LoadLocal;

            BindingContext = model;
            GroupInfoList.ItemsSource = model.Groups;

            if (!Refresh)
            {
                await Navigation.PopPopupAsync();
            }

            GroupInfoList.IsRefreshing = false;
            //if (MasterPage.Instance.IsDetailSet == false)
            //{
            //    MasterPage.Instance.IsDetailSet = true;
            //}else if
            //MasterPage.Instance.Detail = new NavigationPage(this);

        }
        private async void GroupSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                GroupInfo group = GroupInfoList.SelectedItem as GroupInfo;
                GroupInfoList.SelectedItem = null;
                await Navigation.PushPopupAsync(new LoadingModal());
                Schedule Schedule = new Schedule();
                if (model.IsLocal)
                {
                    Schedule = Schedules.Where(x => x.Group.Index == group.Index).FirstOrDefault();
                }
                else
                {
                    Schedule = await Controller.Instance.GetShedules(facilityIndex, group.Index);
                }

                await MasterPage.Instance.Detail.Navigation.PushAsync(new SchedulePage(Schedule, this));

                await Navigation.PopPopupAsync();


            }
        }

        private void OpenClick(object sender, EventArgs e)
        {
            MasterPage.Instance.Detail.Navigation.PushAsync(new FacilityListPage());
        }

        private void GroupInfoList_Refreshing(object sender, EventArgs e)
        {
            OnLoaded(model.IsLocal, false, facilityIndex);
        }
        public void Refresh()
        {
            if (!isFirstLod && model.IsLocal)
            {
                OnLoaded(model.IsLocal, true, facilityIndex);
            }
            else
            {
                isFirstLod = false;
            }

            //base.OnAppearing();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new SettingsModal());
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var t = SearchBar.Text.ToLower();
            if (t == string.Empty)
            {
                GroupInfoList.ItemsSource = model.Groups;
            }
            var result = model.Groups.Where(x => x.Name.ToLower().Contains(t));

            var r = new ObservableCollection<GroupInfo>(result);

            GroupInfoList.ItemsSource = r;
        }
    }
}