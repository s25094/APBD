using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevenueRecognitionSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_UpfrontContracts_UpfrontContractContractId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_UpfrontContractContractId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "UpfrontContractContractId",
                table: "Discounts");

            migrationBuilder.CreateTable(
                name: "DiscountUpfrontContract",
                columns: table => new
                {
                    DiscountsDiscountId = table.Column<int>(type: "int", nullable: false),
                    UpfrontContractsContractId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountUpfrontContract", x => new { x.DiscountsDiscountId, x.UpfrontContractsContractId });
                    table.ForeignKey(
                        name: "FK_DiscountUpfrontContract_Discounts_DiscountsDiscountId",
                        column: x => x.DiscountsDiscountId,
                        principalTable: "Discounts",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscountUpfrontContract_UpfrontContracts_UpfrontContractsContractId",
                        column: x => x.UpfrontContractsContractId,
                        principalTable: "UpfrontContracts",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountUpfrontContract_UpfrontContractsContractId",
                table: "DiscountUpfrontContract",
                column: "UpfrontContractsContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountUpfrontContract");

            migrationBuilder.AddColumn<int>(
                name: "UpfrontContractContractId",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_UpfrontContractContractId",
                table: "Discounts",
                column: "UpfrontContractContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_UpfrontContracts_UpfrontContractContractId",
                table: "Discounts",
                column: "UpfrontContractContractId",
                principalTable: "UpfrontContracts",
                principalColumn: "ContractId");
        }
    }
}
