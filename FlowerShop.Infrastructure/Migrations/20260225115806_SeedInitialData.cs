using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 25, 11, 58, 2, 641, DateTimeKind.Utc).AddTicks(9964), null, false, "Admin", null, null },
                    { 2, new DateTime(2026, 2, 25, 11, 58, 2, 642, DateTimeKind.Utc).AddTicks(460), null, false, "Customer", null, null }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 25, 11, 58, 2, 642, DateTimeKind.Utc).AddTicks(6160), null, "Hoa chúc mừng hồng phát", false, "Hoa Khai Trương", null, null },
                    { 2, new DateTime(2026, 2, 25, 11, 58, 2, 642, DateTimeKind.Utc).AddTicks(6618), null, "Hoa cho ngày trọng đại", false, "Hoa Cưới", null, null },
                    { 3, new DateTime(2026, 2, 25, 11, 58, 2, 642, DateTimeKind.Utc).AddTicks(6620), null, "Quà tặng ý nghĩa", false, "Hoa Sinh Nhật", null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "CreatedBy", "Description", "IsDeleted", "Name", "Price", "Stock", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 2, 25, 11, 58, 2, 642, DateTimeKind.Utc).AddTicks(8057), null, "Bó hoa hồng đỏ lãng mạn", false, "Bó Hoa Hồng Đỏ", 500000m, 20, null, null },
                    { 2, 3, new DateTime(2026, 2, 25, 11, 58, 2, 642, DateTimeKind.Utc).AddTicks(9279), null, "Giỏ hoa tươi tắn như ánh nắng", false, "Giỏ Hoa Hướng Dương", 350000m, 15, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
