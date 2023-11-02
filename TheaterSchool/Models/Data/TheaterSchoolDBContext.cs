using Microsoft.EntityFrameworkCore;

namespace TheaterSchool.Models.Data;

public partial class TheaterSchoolDBContext : DbContext
{
    public TheaterSchoolDBContext()
    {
    }

    public TheaterSchoolDBContext(DbContextOptions<TheaterSchoolDBContext> options) : base(options)
    {
    }

    public virtual DbSet<Performance> Performance { get; set; }
    public virtual DbSet<PhysicalPersons> PhysicalPersons { get; set; }
    public virtual DbSet<Student> Student { get; set; }
    public virtual DbSet<StudentPerformance> StudentPerformance { get; set; }
    public virtual DbSet<StudentSubject> StudentSubject { get; set; }
    public virtual DbSet<Subject> Subject { get; set; }
    public virtual DbSet<Teacher> Teacher { get; set; }
    public virtual DbSet<TeacherPerformance> TeacherPerformance { get; set; }
    public virtual DbSet<TeacherSubject> TeacherSubject { get; set; }
    public virtual DbSet<Lesson> Lesson { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HI86PTN\\SQLEXPRESS;Database=TheaterSchoolDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False",
            x => x.UseDateOnlyTimeOnly());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Performance>(entity =>
        {
            entity.HasIndex(e => e.PerformanceID, "IX_PerformanceID");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);
            entity.Property(e => e.Genre)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.PerformanceName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Requisite).HasMaxLength(500);
        });

        modelBuilder.Entity<PhysicalPersons>(entity =>
        {
            entity.HasIndex(e => e.ID, "IX_PhysicalPersonID");

            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(11);
            entity.Property(e => e.SecondName)
                .IsRequired()
                .HasMaxLength(30);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.PhysicalPersonID);

            entity.HasIndex(e => e.PhysicalPersonID, "IX_Student_PhysicalPersonID");

            entity.Property(e => e.PhysicalPersonID).ValueGeneratedNever();
            entity.Property(e => e.Group)
                .IsRequired()
                .HasMaxLength(10);

            entity.HasOne(d => d.PhysicalPerson).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.PhysicalPersonID)
                .HasConstraintName("FK_PhysicalPerson_Student");
        });

        modelBuilder.Entity<StudentPerformance>(entity =>
        {
            entity.HasKey(e => new { e.StudentID, e.PerformanceID });

            entity.HasIndex(e => e.PerformanceID, "IX_StudentPerformance_PerformanceID");

            entity.HasOne(d => d.Performance).WithMany(p => p.StudentPerformance)
                .HasForeignKey(d => d.PerformanceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentPerformance_Performance");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentPerformance)
                .HasForeignKey(d => d.StudentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentPerformance_Student");
        });

        modelBuilder.Entity<StudentSubject>(entity =>
        {
            entity.HasKey(e => new { e.StudentID, e.SubjectID });

            entity.HasIndex(e => e.SubjectID, "IX_StudentSubject_SubjectID");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentSubject)
                .HasForeignKey(d => d.StudentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentSubject_Student");

            entity.HasOne(d => d.Subject).WithMany(p => p.StudentSubject)
                .HasForeignKey(d => d.SubjectID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentSubject_Subject");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasIndex(e => e.SubjectID, "IX_SubjectID");

            entity.Property(e => e.SubjectName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.PhysicalPersonID);

            entity.HasIndex(e => e.PhysicalPersonID, "IX_Teacher_PhysicalPersonID");

            entity.Property(e => e.PhysicalPersonID).ValueGeneratedNever();
            entity.Property(e => e.Experience)
                .IsRequired()
                .HasMaxLength(2);
            entity.Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(30);

            entity.HasOne(d => d.PhysicalPerson).WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(d => d.PhysicalPersonID)
                .HasConstraintName("FK_PhysicalPerson_Teacher");
        });

        modelBuilder.Entity<TeacherPerformance>(entity =>
        {
            entity.HasKey(e => new { e.TeacherID, e.PerformanceID });

            entity.HasIndex(e => e.PerformanceID, "IX_TeacherPerformance_PerformanceID");

            entity.HasOne(d => d.Performance).WithMany(p => p.TeacherPerformance)
                .HasForeignKey(d => d.PerformanceID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherPerformance_Performance");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherPerformance)
                .HasForeignKey(d => d.TeacherID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherPerformance_Teacher");
        });

        modelBuilder.Entity<TeacherSubject>(entity =>
        {
            entity.HasKey(e => new { e.TeacherID, e.SubjectID });

            entity.HasIndex(e => e.SubjectID, "IX_TeacherSubject_SubjectID");

            entity.HasOne(d => d.Subject).WithMany(p => p.TeacherSubject)
                .HasForeignKey(d => d.SubjectID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherSubject_Subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherSubject)
                .HasForeignKey(d => d.TeacherID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TeacherSubject_Teacher");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonID).HasName("PK_Timetable");

            entity.HasIndex(e => e.LessonID, "IX_TimetableID");

            entity.HasIndex(e => e.TeacherID, "IX_Timetable_TeacherID");

            entity.Property(e => e.DayOfTheWeek)
                .IsRequired()
                .HasMaxLength(11);
            entity.Property(e => e.PeriodNumber)
                .IsRequired()
                .HasMaxLength(1);
            entity.Property(e => e.ClassRoom)
                .IsRequired()
                .HasMaxLength(3);

            entity.HasOne(d => d.Subject).WithMany(p => p.Lesson)
                .HasForeignKey(d => d.SubjectID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lesson_Subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Lesson)
                .HasForeignKey(d => d.TeacherID)
                .HasConstraintName("FK_Lesson_Teacher");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}