namespace TheaterSchool.Models;

public partial class StudentSubject
{
    public int StudentID { get; set; }

    public int SubjectID { get; set; }

    public virtual Student Student { get; set; }

    public virtual Subject Subject { get; set; }
}