using Microsoft.EntityFrameworkCore.Migrations;

namespace Cryptography.Migrations
{
    public partial class Cryptography : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    FamilyName = table.Column<string>(maxLength: 30, nullable: false),
                    UserName = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    FamilyName = table.Column<string>(maxLength: 30, nullable: false),
                    UserName = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 255, nullable: false),
                    TeacherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeacherCiphers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExactTime = table.Column<string>(maxLength: 20, nullable: false),
                    MethodName = table.Column<string>(maxLength: 255, nullable: false),
                    EncodeOrDecode = table.Column<string>(maxLength: 1, nullable: false),
                    InputMessage = table.Column<string>(maxLength: 255, nullable: false),
                    InputArgs = table.Column<string>(maxLength: 255, nullable: false),
                    Result = table.Column<string>(maxLength: 255, nullable: false),
                    TeacherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCiphers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherCiphers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentCiphers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExactTime = table.Column<string>(maxLength: 20, nullable: false),
                    MethodName = table.Column<string>(maxLength: 255, nullable: false),
                    EncodeOrDecode = table.Column<string>(maxLength: 1, nullable: false),
                    InputMessage = table.Column<string>(maxLength: 255, nullable: false),
                    InputArgs = table.Column<string>(maxLength: 255, nullable: false),
                    Result = table.Column<string>(maxLength: 255, nullable: false),
                    StudentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCiphers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCiphers_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCiphers_StudentId",
                table: "StudentCiphers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_TeacherId",
                table: "Students",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCiphers_TeacherId",
                table: "TeacherCiphers",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCiphers");

            migrationBuilder.DropTable(
                name: "TeacherCiphers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
