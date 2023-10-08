using System.ComponentModel;

namespace TheaterSchool.Models;

public partial class Student
{
    [DisplayName("Идентификатор студента")]
    public int PhysicalPersonID { get; set; }

    [DisplayName("Курс")]
    public int Course { get; set; }

    [DisplayName("Группа")]
    public string Group { get; set; }

    public virtual PhysicalPersons PhysicalPerson { get; set; }

    public virtual ICollection<StudentPerformance> StudentPerformance { get; set; } = new List<StudentPerformance>();

    public virtual ICollection<StudentSubject> StudentSubject { get; set; } = new List<StudentSubject>();
}