using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webbshoppen.Migrations
{
    public partial class ChangedPayments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardPaymentOrder");

            migrationBuilder.DropTable(
                name: "InvoicePaymentOrder");

            migrationBuilder.DropTable(
                name: "CardPayments");

            migrationBuilder.DropTable(
                name: "InvoicePayments");

            migrationBuilder.RenameColumn(
                name: "HomeDelivery",
                table: "Shippings",
                newName: "DeliveryOption");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentOption = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderPayment_PaymentsId",
                table: "OrderPayment",
                column: "PaymentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPayment");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.RenameColumn(
                name: "DeliveryOption",
                table: "Shippings",
                newName: "HomeDelivery");

            migrationBuilder.CreateTable(
                name: "CardPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<long>(type: "bigint", nullable: false),
                    CardOwnerName = table.Column<int>(type: "int", nullable: false),
                    CvvCode = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardPaymentOrder",
                columns: table => new
                {
                    CardPaymentsId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPaymentOrder", x => new { x.CardPaymentsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_CardPaymentOrder_CardPayments_CardPaymentsId",
                        column: x => x.CardPaymentsId,
                        principalTable: "CardPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardPaymentOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePaymentOrder",
                columns: table => new
                {
                    InvoicePaymentsId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentOrder", x => new { x.InvoicePaymentsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_InvoicePaymentOrder_InvoicePayments_InvoicePaymentsId",
                        column: x => x.InvoicePaymentsId,
                        principalTable: "InvoicePayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardPaymentOrder_OrdersId",
                table: "CardPaymentOrder",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentOrder_OrdersId",
                table: "InvoicePaymentOrder",
                column: "OrdersId");
        }
    }
}
