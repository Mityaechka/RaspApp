namespace RaspApp.Models
{
    public class Lesson
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Classroom { get; set; }
        public string TeacherName { get; set; }
        public string TeacherPosition { get; set; }
        public string NameClassroom => Name + " (" + Classroom + ")";
        public string TeacherNamePosition => TeacherName + " " + TeacherPosition;
        public Lesson()
        {
        }
    }
}
