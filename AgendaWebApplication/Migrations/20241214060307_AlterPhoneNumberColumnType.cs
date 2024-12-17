using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class AlterPhoneNumberColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellphoneNumber",
                table: "Agenda");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Agenda",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Agenda");

            migrationBuilder.AddColumn<int>(
                name: "CellphoneNumber",
                table: "Agenda",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
