using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RevenueRecognitionSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_UpfrontContracts_ContractId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_UpfrontContracts_Clients_ClientId",
                table: "UpfrontContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_UpfrontContracts_Softwares_SoftwareId",
                table: "UpfrontContracts");

            migrationBuilder.DropTable(
                name: "DiscountUpfrontContract");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UpfrontContracts",
                table: "UpfrontContracts");

            migrationBuilder.RenameTable(
                name: "UpfrontContracts",
                newName: "SoftwareOrders");

            migrationBuilder.RenameColumn(
                name: "ContractId",
                table: "SoftwareOrders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_UpfrontContracts_SoftwareId",
                table: "SoftwareOrders",
                newName: "IX_SoftwareOrders_SoftwareId");

            migrationBuilder.RenameIndex(
                name: "IX_UpfrontContracts_ClientId",
                table: "SoftwareOrders",
                newName: "IX_SoftwareOrders_ClientId");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionOrderId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoftwareOrderOrderId",
                table: "Discounts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "isSigned",
                table: "SoftwareOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Updates",
                table: "SoftwareOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "SoftwareOrders",
                type: "datetime2",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "SoftwareOrders",
                type: "datetime2",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "SoftwareOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "SoftwareOrders",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "QuantityOfRenewalPeriod",
                table: "SoftwareOrders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RenewalPeriod",
                table: "SoftwareOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionName",
                table: "SoftwareOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SoftwareOrders",
                table: "SoftwareOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubscriptionOrderId",
                table: "Payments",
                column: "SubscriptionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_SoftwareOrderOrderId",
                table: "Discounts",
                column: "SoftwareOrderOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SoftwareOrders_DiscountId",
                table: "SoftwareOrders",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_SoftwareOrders_SoftwareOrderOrderId",
                table: "Discounts",
                column: "SoftwareOrderOrderId",
                principalTable: "SoftwareOrders",
                principalColumn: "OrderId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareOrders_Clients_ClientId",
                table: "SoftwareOrders",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareOrders_Discounts_DiscountId",
                table: "SoftwareOrders",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareOrders_Softwares_SoftwareId",
                table: "SoftwareOrders",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "SoftwareId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_SoftwareOrders_SoftwareOrderOrderId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SoftwareOrders_ContractId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SoftwareOrders_SubscriptionOrderId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareOrders_Clients_ClientId",
                table: "SoftwareOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareOrders_Discounts_DiscountId",
                table: "SoftwareOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareOrders_Softwares_SoftwareId",
                table: "SoftwareOrders");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SubscriptionOrderId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_SoftwareOrderOrderId",
                table: "Discounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SoftwareOrders",
                table: "SoftwareOrders");

            migrationBuilder.DropIndex(
                name: "IX_SoftwareOrders_DiscountId",
                table: "SoftwareOrders");

            migrationBuilder.DropColumn(
                name: "SubscriptionOrderId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SoftwareOrderOrderId",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "SoftwareOrders");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "SoftwareOrders");

            migrationBuilder.DropColumn(
                name: "QuantityOfRenewalPeriod",
                table: "SoftwareOrders");

            migrationBuilder.DropColumn(
                name: "RenewalPeriod",
                table: "SoftwareOrders");

            migrationBuilder.DropColumn(
                name: "SubscriptionName",
                table: "SoftwareOrders");

            migrationBuilder.RenameTable(
                name: "SoftwareOrders",
                newName: "UpfrontContracts");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "UpfrontContracts",
                newName: "ContractId");

            migrationBuilder.RenameIndex(
                name: "IX_SoftwareOrders_SoftwareId",
                table: "UpfrontContracts",
                newName: "IX_UpfrontContracts_SoftwareId");

            migrationBuilder.RenameIndex(
                name: "IX_SoftwareOrders_ClientId",
                table: "UpfrontContracts",
                newName: "IX_UpfrontContracts_ClientId");

            migrationBuilder.AlterColumn<int>(
                name: "isSigned",
                table: "UpfrontContracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Updates",
                table: "UpfrontContracts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "UpfrontContracts",
                type: "datetime2",
                maxLength: 100,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "UpfrontContracts",
                type: "datetime2",
                maxLength: 100,
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UpfrontContracts",
                table: "UpfrontContracts",
                column: "ContractId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_UpfrontContracts_ContractId",
                table: "Payments",
                column: "ContractId",
                principalTable: "UpfrontContracts",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UpfrontContracts_Clients_ClientId",
                table: "UpfrontContracts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UpfrontContracts_Softwares_SoftwareId",
                table: "UpfrontContracts",
                column: "SoftwareId",
                principalTable: "Softwares",
                principalColumn: "SoftwareId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
