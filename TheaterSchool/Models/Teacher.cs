using System.ComponentModel;

namespace TheaterSchool.Models;

public partial class Teacher
{
    [DisplayName("Идентификатор преподавателя")]
    public int PhysicalPersonID { get; set; }

    [DisplayName("Должность")]
    public string Position { get; set; }

    [DisplayName("Опыт")]
    public string Experience { get; set; }

    public virtual PhysicalPersons PhysicalPerson { get; set; }

    public virtual ICollection<TeacherPerformance> TeacherPerformance { get; set; } = new List<TeacherPerformance>();

    public virtual ICollection<TeacherSubject> TeacherSubject { get; set; } = new List<TeacherSubject>();

    public virtual ICollection<Lesson> Lesson { get; set; } = new List<Lesson>();
}