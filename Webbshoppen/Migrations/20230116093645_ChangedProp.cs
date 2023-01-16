using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webbshoppen.Migrations
{
    public partial class ChangedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPayment");

            migrationBuilder.DropTable(
                name: "OrderShipping");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShippingId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingId",
                table: "Orders",
                column: "ShippingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shippings_ShippingId",
                table: "Orders",
                column: "ShippingId",
                principalTable: "Shippings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_PaymentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shippings_ShippingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PaymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderPayment",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    PaymentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPayment", x => new { x.OrdersId, x.PaymentsId });
                    table.ForeignKey(
                        name: "FK_OrderPayment_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPayment_Payments_PaymentsId",
                        column: x => x.PaymentsId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderShipping",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ShippingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShipping", x => new { x.OrdersId, x.ShippingsId });
                    table.ForeignKey(
                        name: "FK_OrderShipping_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderShipping_Shippings_ShippingsId",
                        column: x => x.ShippingsId,
                        principalTable: "Shippings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayment_PaymentsId",
                table: "OrderPayment",
                column: "PaymentsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShipping_ShippingsId",
                table: "OrderShipping",
                column: "ShippingsId");
        }
    }
}
