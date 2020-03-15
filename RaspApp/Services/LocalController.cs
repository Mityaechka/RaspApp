using RaspApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
namespace RaspApp.Services
{
    public class LocalController : Controller
    {
        private const string path = "https://drive.google.com/uc?authuser=0&id=1QA9R7Qm6SPnEfNycD4GqZuEiqDbUoAnp&export=download";
        private readonly List<GroupInfo> groups = new List<GroupInfo>()
            {
               new GroupInfo(){
                    Index = "1",
                            Name = "Группа 1"
                        },
               new GroupInfo(){
                    Index = "2",
                            Name = "Группа 2"
                        },
               new GroupInfo(){
                    Index = "3",
                            Name = "Группа 3"
                        }

        };
        private readonly List<Facility> facilities = new List<Facility>()
            {
            new Facility()
            {
                Name = "АУЭС",
                Index = 0,
                Id = 0,
                Details="Подробности"
            },
                        new Facility()
            {
                Name = "ВУЗ 2",
                Index = 1,
                Id = 1,
                Details="Подробности"
            }
            };
        private readonly List<Schedule> schedules = new List<Schedule>();
        private readonly HttpClient client = new HttpClient();
        public LocalController()
        {
            Lesson lesson1 = new Lesson()
            {
                Name = "ООП",
                Type = "лекция",
                Classroom = "A455",
                TeacherName = "Бимурзаев",
                TeacherPosition = "доцент"
            };
            Lesson lesson2 = new Lesson()
            {
                Name = "Web",
                Type = "лаб. работа",
                Classroom = "Б128",
                TeacherName = "Бидахмет",
                TeacherPosition = "PHD"
            };
            List<Day> days = new List<Day>()
            {
                new Day()
                {
                    Name = "Понедельник",
                    TimeRanges = new List<TimeRange>()
                    {
                        new TimeRange()
                        {
                            Index = 1,
                            StartTime = "09:30",
                            EndTime = "10:30",
                            Subgroup1 = lesson1,
                            Subgroup2 = lesson2
                        },
                        new TimeRange()
                        {
                            Index = 1,
                            StartTime = "09:30",
                            EndTime = "10:30",
                            Subgroup1 = lesson1,
                            Subgroup2 = lesson2
                        },
                        new TimeRange()
                        {
                            Index = 1,
                            StartTime = "09:30",
                            EndTime = "10:30",
                            Subgroup1 = lesson1,
                            Subgroup2 = lesson2
                        }
                    }
                },
                 new Day()
                {
                    Name = "Вторник",
                    TimeRanges = new List<TimeRange>()
                    {
                        new TimeRange()
                        {
                            Index = 1,
                            StartTime = "09:30",
                            EndTime = "10:30",
                            Subgroup1 = lesson1,
                            Subgroup2 = lesson2
                        },
                        new TimeRange()
                        {
                            Index = 1,
                            StartTime = "09:30",
                            EndTime = "10:30",
                            UnionLesson = lesson1
                        },
                        new TimeRange()
                        {
                            Index = 1,
                            StartTime = "09:30",
                            EndTime = "10:30",
                            Subgroup1 = lesson1,
                            Subgroup2 = lesson2
                        }
                    }
                },
                  new Day()
                {
                    Name = "Четверг",
                    TimeRanges = new List<TimeRange>()
                    {
                        new TimeRange()
                        {
                            Index = 1,
                            StartTime = "09:30",
                            EndTime = "10:30",
                            Subgroup1 = lesson1,
                            Subgroup2 = lesson2
                        },
                        new TimeRange()
                        {
                            Index = 1,
                            StartTime = "09:30",
                            EndTime = "10:30",
                            Subgroup2 = lesson2
                        },
                        new TimeRange()
                        {
                            Index = 1,
                            StartTime = "09:30",
                            EndTime = "10:30",
                            Subgroup1 = lesson1
                        }
                    }
                },
            };
            schedules.Add(new Schedule()
            {
                Group = groups[0],
                Days = days,
                Facility = facilities[0]
            });
            schedules.Add(new Schedule()
            {
                Group = groups[1],
                Days = days,
                Facility = facilities[1]
            });
        }

        public override async Task<Schedule> GetShedules(int facilityIndex, string scheduleIndex)
        {
            await Task.Delay(100);
            return schedules.Where(x => x.Group.Index == scheduleIndex && x.Facility.Index == facilityIndex).FirstOrDefault();
        }

        public override async Task<List<Facility>> GetFacilities(bool loadTestFacilities = true)
        {
            await Task.Delay(100);
            return facilities;
        }

        public override async Task<List<GroupInfo>> GetShedulesList(int facilityIndex)
        {
            await Task.Delay(100);
            Facility fasility = facilities.Where(x => x.Index == facilityIndex).FirstOrDefault();
            IEnumerable<Schedule> sortShediles = schedules.Where(x => x.Facility == fasility);
            return sortShediles.Select<Schedule, GroupInfo>(x => x.Group).ToList();
        }
    }
}
