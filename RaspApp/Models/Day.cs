using System.Collections.Generic;


namespace RaspApp.Models
{
    public class Day
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<TimeRange> TimeRanges { get; set; }

        public Day()
        {
        }
    }
}
