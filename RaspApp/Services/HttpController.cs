using Newtonsoft.Json;
using RaspApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RaspApp.Services
{
    public class HttpController : Controller
    {
        private readonly HttpClient client;

        public HttpController() : base()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri($"{App.AzureBackendUrl}/")
            };
        }

        private bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public override async Task<List<Facility>> GetFacilities(bool loadTestFacilities = true)
        {
            List<Facility> groups = null;
            if (IsConnected)
            {
                groups = new List<Facility>();
                string json = await client.GetStringAsync($"api/parser/" + loadTestFacilities);
                groups = await Task.Run(() => JsonConvert.DeserializeObject<List<Facility>>(json));
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Проверьте подключение к Интернету");
            }

            return groups;
        }

        public override async Task<List<GroupInfo>> GetShedulesList(int facilityIndex)
        {
            List<GroupInfo> groups = null;
            if (IsConnected)
            {
                groups = new List<GroupInfo>();
                string json = await client.GetStringAsync($"api/parser/schedule/" + facilityIndex);
                groups = await Task.Run(() => JsonConvert.DeserializeObject<List<GroupInfo>>(json));
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Проверьте подключение к Интернету");
            }

            return groups;
        }
        public override async Task<Schedule> GetShedules(int facilityIndex, string scheduleIndex)
        {
            Schedule schedule = null;
            if (IsConnected)
            {
                string json = await client.GetStringAsync($"api/parser/schedule/" + facilityIndex + "/" + scheduleIndex);
                schedule = await Task.Run(() => JsonConvert.DeserializeObject<Schedule>(json));
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert("Проверьте подключение к Интернету");
            }

            return schedule;
        }


    }
}
