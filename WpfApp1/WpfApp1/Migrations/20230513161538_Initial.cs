using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WpfApp1.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    address = table.Column<string>(type: "text", nullable: true),
                    ransom = table.Column<decimal>(type: "money", nullable: true),
                    discount = table.Column<int>(type: "integer", nullable: true),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clients_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "producttype",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    productname = table.Column<string>(type: "text", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("producttype_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Acess = table.Column<int>(type: "integer", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Documents = table.Column<string>(type: "json", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Users_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('orders_id_seq1'::regclass)"),
                    clientid = table.Column<int>(type: "integer", nullable: false),
                    sum = table.Column<decimal>(type: "money", nullable: false),
                    dateofcreate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orders_pkey1", x => x.id);
                    table.ForeignKey(
                        name: "Orders",
                        column: x => x.clientid,
                        principalTable: "clients",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    producttypeid = table.Column<int>(type: "integer", nullable: false),
                    nameproduct = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    countofprod = table.Column<int>(type: "integer", nullable: false),
                    producername = table.Column<string>(type: "text", nullable: true),
                    model = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("products_pkey", x => x.id);
                    table.ForeignKey(
                        name: "products",
                        column: x => x.producttypeid,
                        principalTable: "producttype",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('orders_id_seq'::regclass)"),
                    productid = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('orders_productid_seq'::regclass)"),
                    orderid = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('orders_orderid_seq'::regclass)"),
                    productcount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orders_pkey", x => x.id);
                    table.ForeignKey(
                        name: "Orders",
                        column: x => x.orderid,
                        principalTable: "order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "orders",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "soldproducts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientid = table.Column<int>(type: "integer", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    dateofsold = table.Column<DateOnly>(type: "date", nullable: false),
                    orderid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("soldproducts_pkey", x => x.id);
                    table.ForeignKey(
                        name: "SoldProducts",
                        column: x => x.clientid,
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "soldproducts",
                        column: x => x.productid,
                        principalTable: "products",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_orderid",
                table: "order",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_order_productid",
                table: "order",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_clientid",
                table: "orders",
                column: "clientid");

            migrationBuilder.CreateIndex(
                name: "IX_products_producttypeid",
                table: "products",
                column: "producttypeid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "product",
                table: "products",
                column: "producttypeid");

            migrationBuilder.CreateIndex(
                name: "IX_soldproducts_clientid",
                table: "soldproducts",
                column: "clientid");

            migrationBuilder.CreateIndex(
                name: "IX_soldproducts_productid",
                table: "soldproducts",
                column: "productid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "soldproducts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "producttype");
        }
    }
}
