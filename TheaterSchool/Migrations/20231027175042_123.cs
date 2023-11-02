using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheaterSchool.Migrations
{
    /// <inheritdoc />
    public partial class _123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherInfo",
                columns: table => new
                {
                    TeacherPhysicalPersonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_TeacherInfo_Teacher_TeacherPhysicalPersonID",
                        column: x => x.TeacherPhysicalPersonID,
                        principalTable: "Teacher",
                        principalColumn: "PhysicalPersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherInfo_TeacherPhysicalPersonID",
                table: "TeacherInfo",
                column: "TeacherPhysicalPersonID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherInfo");
        }
    }
}
