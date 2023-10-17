using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheaterSchool.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    PerformanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformanceName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Duration = table.Column<TimeOnly>(type: "time", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Requisite = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.PerformanceID);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalPersons",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Sex = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalPersons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duration = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    PhysicalPersonID = table.Column<int>(type: "int", nullable: false),
                    Course = table.Column<int>(type: "int", nullable: false),
                    Group = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.PhysicalPersonID);
                    table.ForeignKey(
                        name: "FK_PhysicalPerson_Student",
                        column: x => x.PhysicalPersonID,
                        principalTable: "PhysicalPersons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    PhysicalPersonID = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.PhysicalPersonID);
                    table.ForeignKey(
                        name: "FK_PhysicalPerson_Teacher",
                        column: x => x.PhysicalPersonID,
                        principalTable: "PhysicalPersons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentPerformance",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    PerformanceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPerformance", x => new { x.StudentID, x.PerformanceID });
                    table.ForeignKey(
                        name: "FK_StudentPerformance_Performance",
                        column: x => x.PerformanceID,
                        principalTable: "Performance",
                        principalColumn: "PerformanceID");
                    table.ForeignKey(
                        name: "FK_StudentPerformance_Student",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "PhysicalPersonID");
                });

            migrationBuilder.CreateTable(
                name: "StudentSubject",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSubject", x => new { x.StudentID, x.SubjectID });
                    table.ForeignKey(
                        name: "FK_StudentSubject_Student",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "PhysicalPersonID");
                    table.ForeignKey(
                        name: "FK_StudentSubject_Subject",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "SubjectID");
                });

            migrationBuilder.CreateTable(
                name: "TeacherPerformance",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    PerformanceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherPerformance", x => new { x.TeacherID, x.PerformanceID });
                    table.ForeignKey(
                        name: "FK_TeacherPerformance_Performance",
                        column: x => x.PerformanceID,
                        principalTable: "Performance",
                        principalColumn: "PerformanceID");
                    table.ForeignKey(
                        name: "FK_TeacherPerformance_Teacher",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "PhysicalPersonID");
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubject",
                columns: table => new
                {
                    TeacherID = table.Column<int>(type: "int", nullable: false),
                    SubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubject", x => new { x.TeacherID, x.SubjectID });
                    table.ForeignKey(
                        name: "FK_TeacherSubject_Subject",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "SubjectID");
                    table.ForeignKey(
                        name: "FK_TeacherSubject_Teacher",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "PhysicalPersonID");
                });

            migrationBuilder.CreateTable(
                name: "Timetable",
                columns: table => new
                {
                    TimetableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfTheWeek = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PeriodNumber = table.Column<int>(type: "int", nullable: false),
                    ClassRoom = table.Column<int>(type: "int", nullable: false),
                    TeacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timetable", x => x.TimetableID);
                    table.ForeignKey(
                        name: "FK_Timetable_Teacher",
                        column: x => x.TeacherID,
                        principalTable: "Teacher",
                        principalColumn: "PhysicalPersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTimetable",
                columns: table => new
                {
                    SubjectID = table.Column<int>(type: "int", nullable: false),
                    TimetableID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTimetable", x => new { x.SubjectID, x.TimetableID });
                    table.ForeignKey(
                        name: "FK_SubjectTimetable_Subject",
                        column: x => x.SubjectID,
                        principalTable: "Subject",
                        principalColumn: "SubjectID");
                    table.ForeignKey(
                        name: "FK_SubjectTimetable_Timetable",
                        column: x => x.TimetableID,
                        principalTable: "Timetable",
                        principalColumn: "TimetableID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceID",
                table: "Performance",
                column: "PerformanceID");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalPersonID",
                table: "PhysicalPersons",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_PhysicalPersonID",
                table: "Student",
                column: "PhysicalPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPerformance_PerformanceID",
                table: "StudentPerformance",
                column: "PerformanceID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSubject_SubjectID",
                table: "StudentSubject",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectID",
                table: "Subject",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTimetable_TimetableID",
                table: "SubjectTimetable",
                column: "TimetableID");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_PhysicalPersonID",
                table: "Teacher",
                column: "PhysicalPersonID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherPerformance_PerformanceID",
                table: "TeacherPerformance",
                column: "PerformanceID");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubject_SubjectID",
                table: "TeacherSubject",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Timetable_TeacherID",
                table: "Timetable",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_TimetableID",
                table: "Timetable",
                column: "TimetableID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentPerformance");

            migrationBuilder.DropTable(
                name: "StudentSubject");

            migrationBuilder.DropTable(
                name: "SubjectTimetable");

            migrationBuilder.DropTable(
                name: "TeacherPerformance");

            migrationBuilder.DropTable(
                name: "TeacherSubject");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Timetable");

            migrationBuilder.DropTable(
                name: "Performance");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "PhysicalPersons");
        }
    }
}
