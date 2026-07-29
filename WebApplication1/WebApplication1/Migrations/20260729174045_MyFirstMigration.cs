using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RespondentDataTracker.Migrations
{
    /// <inheritdoc />
    public partial class MyFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JobPosition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkExperience = table.Column<double>(type: "float", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    PersonAdress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonAdress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonAdress_HomeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
