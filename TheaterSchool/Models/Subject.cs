using System.ComponentModel;

namespace TheaterSchool.Models;

public partial class Subject
{
    [DisplayName("Идентификатор предмета")]
    public int SubjectID { get; set; }

    [DisplayName("Название предмета")]
    public string SubjectName { get; set; }

    [DisplayName("Продолжительность")]
    public TimeOnly Duration { get; set; }

    public virtual ICollection<StudentSubject> StudentSubject { get; set; } = new List<StudentSubject>();

    public virtual ICollection<Lesson> Lesson { get; set; } = new List<Lesson>();

    public virtual ICollection<TeacherSubject> TeacherSubject { get; set; } = new List<TeacherSubject>();
}