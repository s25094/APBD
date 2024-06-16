using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevenueRecognitionSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UpfrontContracts_Discounts_DiscountId",
                table: "UpfrontContracts");

            migrationBuilder.DropIndex(
                name: "IX_UpfrontContracts_DiscountId",
                table: "UpfrontContracts");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "UpfrontContracts");

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
                name: "DiscountId",
                table: "UpfrontContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UpfrontContracts_DiscountId",
                table: "UpfrontContracts",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UpfrontContracts_Discounts_DiscountId",
                table: "UpfrontContracts",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
