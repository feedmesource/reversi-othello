using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConfigurationsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Configs_GameConfigDtoId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_States_Configs_ConfigId",
                table: "States");

            migrationBuilder.DropPrimaryKey(
                name: "PK_States",
                table: "States");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Configs",
                table: "Configs");

            migrationBuilder.RenameTable(
                name: "States",
                newName: "GameStates");

            migrationBuilder.RenameTable(
                name: "Configs",
                newName: "Configurations");

            migrationBuilder.RenameIndex(
                name: "IX_States_ConfigId",
                table: "GameStates",
                newName: "IX_GameStates_ConfigId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameStates",
                table: "GameStates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configurations",
                table: "Configurations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameStates_Configurations_ConfigId",
                table: "GameStates",
                column: "ConfigId",
                principalTable: "Configurations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Configurations_GameConfigDtoId",
                table: "Players",
                column: "GameConfigDtoId",
                principalTable: "Configurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameStates_Configurations_ConfigId",
                table: "GameStates");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Configurations_GameConfigDtoId",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameStates",
                table: "GameStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Configurations",
                table: "Configurations");

            migrationBuilder.RenameTable(
                name: "GameStates",
                newName: "States");

            migrationBuilder.RenameTable(
                name: "Configurations",
                newName: "Configs");

            migrationBuilder.RenameIndex(
                name: "IX_GameStates_ConfigId",
                table: "States",
                newName: "IX_States_ConfigId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_States",
                table: "States",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Configs",
                table: "Configs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Configs_GameConfigDtoId",
                table: "Players",
                column: "GameConfigDtoId",
                principalTable: "Configs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_States_Configs_ConfigId",
                table: "States",
                column: "ConfigId",
                principalTable: "Configs",
                principalColumn: "Id");
        }
    }
}
