namespace TheaterSchool.Models;

public partial class TeacherSubject
{
    public int TeacherID { get; set; }

    public int SubjectID { get; set; }

    public virtual Subject Subject { get; set; }

    public virtual Teacher Teacher { get; set; }
}