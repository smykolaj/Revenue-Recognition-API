using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class deletedManyToManyDiscountContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDiscounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractDiscounts",
                columns: table => new
                {
                    IdContract = table.Column<long>(type: "bigint", nullable: false),
                    IdDiscount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDiscounts", x => new { x.IdContract, x.IdDiscount });
                    table.ForeignKey(
                        name: "FK_ContractDiscounts_Contracts_IdContract",
                        column: x => x.IdContract,
                        principalTable: "Contracts",
                        principalColumn: "IdContract",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractDiscounts_Discounts_IdDiscount",
                        column: x => x.IdDiscount,
                        principalTable: "Discounts",
                        principalColumn: "IdDiscount",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDiscounts_IdDiscount",
                table: "ContractDiscounts",
                column: "IdDiscount");
        }
    }
}
