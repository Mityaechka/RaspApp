
using RaspApp.Models;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace RaspApp.Services
{
    public class Controller
    {
        public static Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Controller();
                }
                return instance;
            }
            set => instance = value;
        }

        private static Controller instance { get; set; }
        protected Controller() { }
        public virtual async Task<List<GroupInfo>> GetShedulesList(int facilityIndex)
        {
            return null;
        }
        public virtual async Task<Schedule> GetShedules(int facilityIndex, string scheduleIndex)
        {
            return null;
        }
        public virtual async Task<List<Facility>> GetFacilities(bool loadTestFacilities = true)
        {
            return null;
        }
    }
}
