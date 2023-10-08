namespace TheaterSchool.Models;

public partial class SubjectTimetable
{
    public int SubjectID { get; set; }

    public int TimetableID { get; set; }

    public virtual Subject Subject { get; set; }

    public virtual Timetable Timetable { get; set; }
}