namespace TheaterSchool.Models
{
    public class TeacherInfo
    {
        public Teacher Teacher { get; set; }
        public List<Timetable> Timetables { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Performance> Performances { get; set; }
    }
}
