using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class mig001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "SubjectClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubjectClasses_TeacherId",
                table: "SubjectClasses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectClasses_Teachers_TeacherId",
                table: "SubjectClasses",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubjectClasses_Teachers_TeacherId",
                table: "SubjectClasses");

            migrationBuilder.DropIndex(
                name: "IX_SubjectClasses_TeacherId",
                table: "SubjectClasses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "SubjectClasses");
        }
    }
}
