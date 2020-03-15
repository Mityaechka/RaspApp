using Newtonsoft.Json;
using Plugin.Settings;
using System.Collections.Generic;
using System.Linq;

namespace RaspApp.Models
{
    public class Schedule
    {
        public GroupInfo Group { get; set; }
        public List<Day> Days { get; set; }
        public Facility Facility { get; set; }
        public Schedule()
        {

        }
        public bool IsSaved
        {
            get
            {
                List<Schedule> saved = JsonConvert.DeserializeObject<List<Schedule>>(
                    CrossSettings.Current.GetValueOrDefault("Saved", ""));
                return saved != null && saved.Where(x => x.Group.Index == Group.Index).Count() == 1;
            }
        }
    }
}
