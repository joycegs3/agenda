using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaWebApplication.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUsserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agenda_User_UserId",
                table: "Agenda");

            migrationBuilder.DropIndex(
                name: "IX_Agenda_UserId",
                table: "Agenda");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Agenda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Agenda",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Agenda_UserId",
                table: "Agenda",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agenda_User_UserId",
                table: "Agenda",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
