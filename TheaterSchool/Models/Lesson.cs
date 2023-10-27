using System.ComponentModel;

namespace TheaterSchool.Models;

public partial class Lesson
{
    [DisplayName("Идентификатор занятия")]
    public int LessonID { get; set; }

    [DisplayName("День недели")]
    public string DayOfTheWeek { get; set; }

    [DisplayName("Номер пары")]
    public int PeriodNumber { get; set; }

    [DisplayName("Аудитория")]
    public int ClassRoom { get; set; }

    public int TeacherID { get; set; }
    public int SubjectID { get; set; }

    public virtual Subject Subject { get; set; }
    public virtual Teacher Teacher { get; set; }
}