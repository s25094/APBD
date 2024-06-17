using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevenueRecognitionSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration26 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SoftwareOrders_ContractId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SoftwareOrders_SubscriptionOrderId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SubscriptionOrderId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SubscriptionOrderId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "Payments",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_ContractId",
                table: "Payments",
                newName: "IX_Payments_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SoftwareOrders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "SoftwareOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SoftwareOrders_OrderId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Payments",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                newName: "IX_Payments_ContractId");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionOrderId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubscriptionOrderId",
                table: "Payments",
                column: "SubscriptionOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SoftwareOrders_ContractId",
                table: "Payments",
                column: "ContractId",
                principalTable: "SoftwareOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SoftwareOrders_SubscriptionOrderId",
                table: "Payments",
                column: "SubscriptionOrderId",
                principalTable: "SoftwareOrders",
                principalColumn: "OrderId");
        }
    }
}
