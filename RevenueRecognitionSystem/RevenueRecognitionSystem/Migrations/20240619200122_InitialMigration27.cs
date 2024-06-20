using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevenueRecognitionSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SoftwareOrderOrderId",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_SoftwareOrderOrderId",
                table: "Discounts",
                column: "SoftwareOrderOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_SoftwareOrders_SoftwareOrderOrderId",
                table: "Discounts",
                column: "SoftwareOrderOrderId",
                principalTable: "SoftwareOrders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_SoftwareOrders_SoftwareOrderOrderId",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_SoftwareOrderOrderId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "SoftwareOrderOrderId",
                table: "Discounts");
        }
    }
}
