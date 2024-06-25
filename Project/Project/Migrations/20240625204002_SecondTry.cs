using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class SecondTry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshTokenExp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategory = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    IdCompany = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Krs = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.IdCompany);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    IdDiscount = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Offer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.IdDiscount);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    IdIndividual = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.IdIndividual);
                });

            migrationBuilder.CreateTable(
                name: "Softwares",
                columns: table => new
                {
                    IdSoftware = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdCategory = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Softwares", x => x.IdSoftware);
                    table.ForeignKey(
                        name: "FK_Softwares_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "IdCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    IdVersion = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdSoftware = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.IdVersion);
                    table.ForeignKey(
                        name: "FK_Versions_Softwares_IdSoftware",
                        column: x => x.IdSoftware,
                        principalTable: "Softwares",
                        principalColumn: "IdSoftware",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    IdContract = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IdIndividual = table.Column<long>(type: "bigint", nullable: true),
                    IdCompany = table.Column<long>(type: "bigint", nullable: true),
                    IdSoftware = table.Column<long>(type: "bigint", nullable: false),
                    IdVersion = table.Column<long>(type: "bigint", nullable: false),
                    ContinuedSupportYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.IdContract);
                    table.ForeignKey(
                        name: "FK_Contracts_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "IdCompany",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Individuals_IdIndividual",
                        column: x => x.IdIndividual,
                        principalTable: "Individuals",
                        principalColumn: "IdIndividual",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Softwares_IdSoftware",
                        column: x => x.IdSoftware,
                        principalTable: "Softwares",
                        principalColumn: "IdSoftware",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Versions_IdVersion",
                        column: x => x.IdVersion,
                        principalTable: "Versions",
                        principalColumn: "IdVersion",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    IdPayment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdContract = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.IdPayment);
                    table.ForeignKey(
                        name: "FK_Payments_Contracts_IdContract",
                        column: x => x.IdContract,
                        principalTable: "Contracts",
                        principalColumn: "IdContract",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDiscounts_IdDiscount",
                table: "ContractDiscounts",
                column: "IdDiscount");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdCompany",
                table: "Contracts",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdIndividual",
                table: "Contracts",
                column: "IdIndividual");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdSoftware",
                table: "Contracts",
                column: "IdSoftware");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_IdVersion",
                table: "Contracts",
                column: "IdVersion");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_IdContract",
                table: "Payments",
                column: "IdContract");

            migrationBuilder.CreateIndex(
                name: "IX_Softwares_IdCategory",
                table: "Softwares",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_IdSoftware",
                table: "Versions",
                column: "IdSoftware");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "ContractDiscounts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropTable(
                name: "Softwares");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
