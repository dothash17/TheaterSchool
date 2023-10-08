namespace TheaterSchool.Models;

public partial class TeacherTimetable
{
    public int TeacherID { get; set; }

    public int TimetableID { get; set; }

    public virtual Teacher Teacher { get; set; }

    public virtual Timetable Timetable { get; set; }
}