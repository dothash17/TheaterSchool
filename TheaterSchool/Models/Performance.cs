using System.ComponentModel;

namespace TheaterSchool.Models;

public partial class Performance
{
    [DisplayName("Идентификатор спектакля")]
    public int PerformanceID { get; set; }

    [DisplayName("Название спектакля")]
    public string PerformanceName { get; set; }

    [DisplayName("Описание")]
    public string Description { get; set; }

    [DisplayName("Жанр")]
    public string Genre { get; set; }

    [DisplayName("Длительность")]
    public TimeOnly Duration { get; set; }

    [DisplayName("Место проведения")]
    public string Location { get; set; }

    [DisplayName("Реквизит")]
    public string? Requisite { get; set; }

    public virtual ICollection<StudentPerformance> StudentPerformance { get; set; } = new List<StudentPerformance>();

    public virtual ICollection<TeacherPerformance> TeacherPerformance { get; set; } = new List<TeacherPerformance>();
}