namespace RaspApp.Models
{
    public class TimeRange
    {
        public int Index { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Lesson Subgroup1 { get; set; }
        public Lesson Subgroup2 { get; set; }
        public Lesson UnionLesson { get; set; }
        public string Time => StartTime + "-" + EndTime;
        public bool IsUnion => UnionLesson != null;
        public int HeightRequest => IsUnion ? 200 : 300;
        public bool IsDivided => UnionLesson == null;
        public bool HasSubroup1 => Subgroup1 != null;
        public bool HasSubroup2 => Subgroup2 != null;
        public bool IsEmpty1 => Subgroup1 == null;
        public bool IsEmpty2 => Subgroup2 == null;
        public bool ShowIndex => Index != -1;
        public bool HideIndex => !ShowIndex;
        public TimeRange()
        {
        }
    }
}
