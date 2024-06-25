using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class OoopsDeletedTableVersions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Versions_IdVersion",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Softwares_IdSoftware",
                table: "Versions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Versions",
                table: "Versions");

            migrationBuilder.RenameTable(
                name: "Versions",
                newName: "Version");

            migrationBuilder.RenameIndex(
                name: "IX_Versions_IdSoftware",
                table: "Version",
                newName: "IX_Version_IdSoftware");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Version",
                table: "Version",
                column: "IdVersion");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Version_IdVersion",
                table: "Contracts",
                column: "IdVersion",
                principalTable: "Version",
                principalColumn: "IdVersion",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Version_Softwares_IdSoftware",
                table: "Version",
                column: "IdSoftware",
                principalTable: "Softwares",
                principalColumn: "IdSoftware",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Version_IdVersion",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Version_Softwares_IdSoftware",
                table: "Version");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Version",
                table: "Version");

            migrationBuilder.RenameTable(
                name: "Version",
                newName: "Versions");

            migrationBuilder.RenameIndex(
                name: "IX_Version_IdSoftware",
                table: "Versions",
                newName: "IX_Versions_IdSoftware");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Versions",
                table: "Versions",
                column: "IdVersion");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Versions_IdVersion",
                table: "Contracts",
                column: "IdVersion",
                principalTable: "Versions",
                principalColumn: "IdVersion",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Softwares_IdSoftware",
                table: "Versions",
                column: "IdSoftware",
                principalTable: "Softwares",
                principalColumn: "IdSoftware",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
