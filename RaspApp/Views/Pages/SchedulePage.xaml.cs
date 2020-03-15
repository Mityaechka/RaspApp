using Newtonsoft.Json;
using Plugin.Settings;
using RaspApp.Models;
using RaspApp.Services;
using RaspApp.ViewModel;
using RaspApp.Views.Modals;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xam.Plugin.TabView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RaspApp.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        public Schedule Schedule { get; set; }

        private readonly Day day;
        private readonly GroupInfoListPage Page;
        public ICommand Refresh { get; set; }
        private static SchedulePage schedulePage { get; set; }
        public bool TabsViewMode = false;
        public SchedulePage(Schedule schedule, GroupInfoListPage page)
        {
            InitializeComponent();
            TabsViewMode = new SettingsViewModel().Model.TabViewMode;
            Refresh = new Command(RefreshClick);
            Page = page;
            Schedule = schedule;
            day = Schedule.Days[0];
            Init();

            if (schedulePage != null && MasterPage.Instance.Detail.Navigation.NavigationStack.Contains(schedulePage))
            {
                MasterPage.Instance.Detail.Navigation.RemovePage(schedulePage);
            }

            schedulePage = this;

        }

        private void Init()
        {
            BindingContext = this;
            if (TabsViewMode)
            {
                DaysTabView.IsVisible = true;
                DaysListView.IsVisible = false;
                for (int i = 0; i < Schedule.Days.Count; i++)
                {
                    DaysTabView.AddTab(new TabItem(Schedule.Days[i].Name,
                        new DayPage(Schedule.Days[i])));
                }
            }
            else
            {
                 DaysTabView.IsVisible = false;
                DaysListView.IsVisible = true;
                DaysListView.ItemsSource = Schedule.Days;
            }
            SetToolBarItems();
        }

        protected override void OnDisappearing()
        {
            Page.Refresh();
            base.OnDisappearing();
        }

        private async void RefreshClick()
        {
            //RefreshLayout.IsRefreshing = false;

            await Navigation.PushPopupAsync(new LoadingModal());

            Schedule s = await Controller.Instance.GetShedules(Schedule.Facility.Index, Schedule.Group.Index);
            if (s != null)
            {
                Schedule = s;
                if (Schedule.IsSaved)
                {
                    SaveClick(null, null);
                }

                await MasterPage.Instance.Detail.Navigation.PushAsync(new SchedulePage(Schedule, Page));
            }
            await Navigation.PopPopupAsync();
        }

        private async void RefreshClick(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();

            await Navigation.PushPopupAsync(new LoadingModal());

            Schedule s = await Controller.Instance.GetShedules(Schedule.Facility.Index, Schedule.Group.Index);
            if (s != null)
            {
                Schedule = s;
                if (Schedule.IsSaved)
                {
                    SaveClick(null, null);
                }

                await MasterPage.Instance.Detail.Navigation.PushAsync(new SchedulePage(Schedule, Page));
            }
            await Navigation.PopPopupAsync();

            //
        }

        private void SaveClick(object sender, EventArgs e)
        {
            List<Schedule> saved = JsonConvert.DeserializeObject<List<Schedule>>(
                CrossSettings.Current.GetValueOrDefault("Saved", ""));
            if (saved == null)
            {
                saved = new List<Schedule>();
            }

            IEnumerable<Schedule> s = saved.Where(x => Schedule.Group.Index == x.Group.Index);
            if (s.Count() == 0)
            {
                saved.Add(Schedule);
            }
            else
            {
                saved[saved.IndexOf(s.First())] = Schedule;
            }
            CrossSettings.Current.AddOrUpdateValue(
                "Saved", JsonConvert.SerializeObject(saved));
            if (sender != null)
            {
                DependencyService.Get<IMessage>().ShortAlert("Расписание сохранено");
            }

            SetToolBarItems();
        }

        private void DeleteClick(object sender, EventArgs e)
        {
            List<Schedule> saved = JsonConvert.DeserializeObject<List<Schedule>>(
            CrossSettings.Current.GetValueOrDefault("Saved", ""));
            if (saved == null)
            {
                return;
            }

            Schedule s = saved.Where(x => Schedule.Group.Index == x.Group.Index).FirstOrDefault();
            if (s != null)
            {
                saved.Remove(s);
                CrossSettings.Current.AddOrUpdateValue(
                    "Saved", JsonConvert.SerializeObject(saved));
            }
            DependencyService.Get<IMessage>().ShortAlert("Расписание удалено");
            SetToolBarItems();
        }
        public void SetToolBarItems()
        {
            ToolbarItems.Clear();
            ToolbarItem info = new ToolbarItem()
            {
                Text = "Информация",
                IconImageSource = "info.png",

            }; 
            ToolbarItem refresh = new ToolbarItem()
            {
                Text = "Обновить",
                IconImageSource = "refresh.png",

            };
            refresh.Clicked += RefreshClick;
            info.Clicked += InfoClick;
            ToolbarItem save = new ToolbarItem()
            {
                Text = "Сохранить",
                IconImageSource = "save.png",

            };
            save.Clicked += SaveClick;
            ToolbarItem delete = new ToolbarItem()
            {
                Text = "Удалить",
                IconImageSource = "delete.png",

            };
            delete.Clicked += DeleteClick;
            ToolbarItems.Add(refresh);
            if (Schedule.IsSaved)
            {
                ToolbarItems.Add(delete);
            }
            else
            {
                ToolbarItems.Add(save);
            }

            ToolbarItems.Add(info);
        }
        private async void InfoClick(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new FacilityModal(Schedule.Facility));
        }

        private void DaysListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DaysListView.SelectedItem = null;
        }

    }
}