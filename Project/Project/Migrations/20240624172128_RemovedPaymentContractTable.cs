using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPaymentContractTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contracts_ContractIdContract",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentContracts");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ContractIdContract",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ContractIdContract",
                table: "Payments");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Payments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "IdContract",
                table: "Payments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdContract",
                table: "Payments",
                column: "IdContract");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contracts_IdContract",
                table: "Payments",
                column: "IdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contracts_IdContract",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_IdContract",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "IdContract",
                table: "Payments");

            migrationBuilder.AddColumn<long>(
                name: "ContractIdContract",
                table: "Payments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentContracts",
                columns: table => new
                {
                    IdPayment = table.Column<int>(type: "int", nullable: false),
                    IdContract = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentContracts", x => new { x.IdPayment, x.IdContract });
                    table.ForeignKey(
                        name: "FK_PaymentContracts_Contracts_IdContract",
                        column: x => x.IdContract,
                        principalTable: "Contracts",
                        principalColumn: "IdContract",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentContracts_Payments_IdPayment",
                        column: x => x.IdPayment,
                        principalTable: "Payments",
                        principalColumn: "IdPayment",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ContractIdContract",
                table: "Payments",
                column: "ContractIdContract");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentContracts_IdContract",
                table: "PaymentContracts",
                column: "IdContract");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contracts_ContractIdContract",
                table: "Payments",
                column: "ContractIdContract",
                principalTable: "Contracts",
                principalColumn: "IdContract");
        }
    }
}
