using System.ComponentModel;

namespace TheaterSchool.Models;

public partial class Timetable
{
    [DisplayName("Идентификатор расписания")]
    public int TimetableID { get; set; }

    [DisplayName("День недели")]
    public string DayOfTheWeek { get; set; }

    [DisplayName("Номер пары")]
    public int PeriodNumber { get; set; }

    [DisplayName("Аудитория")]
    public int ClassRoom { get; set; }

    public virtual ICollection<SubjectTimetable> SubjectTimetable { get; set; } = new List<SubjectTimetable>();

    public virtual ICollection<TeacherTimetable> TeacherTimetable { get; set; } = new List<TeacherTimetable>();
}