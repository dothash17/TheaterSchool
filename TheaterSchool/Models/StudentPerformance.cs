namespace TheaterSchool.Models;

public partial class StudentPerformance
{
    public int StudentID { get; set; }

    public int PerformanceID { get; set; }

    public virtual Performance Performance { get; set; }

    public virtual Student Student { get; set; }
}