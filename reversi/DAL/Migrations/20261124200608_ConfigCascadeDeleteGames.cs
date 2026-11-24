using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConfigCascadeDeleteGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStates_Configurations_ConfigId",
                table: "GameStates");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStates_Configurations_ConfigId",
                table: "GameStates",
                column: "ConfigId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStates_Configurations_ConfigId",
                table: "GameStates");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStates_Configurations_ConfigId",
                table: "GameStates",
                column: "ConfigId",
                principalTable: "Configurations",
                principalColumn: "Id");
        }
    }
}
