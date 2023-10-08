namespace TheaterSchool.Models;

public partial class TeacherPerformance
{
    public int TeacherID { get; set; }

    public int PerformanceID { get; set; }

    public virtual Performance Performance { get; set; }

    public virtual Teacher Teacher { get; set; }
}