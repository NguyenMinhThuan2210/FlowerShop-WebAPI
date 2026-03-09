using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaxDiscountFromVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDiscount",
                table: "Vouchers");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPercent",
                table: "Vouchers",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DiscountPercent",
                table: "Vouchers",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "MaxDiscount",
                table: "Vouchers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
