using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rentACar.Persistence.Migrations
{
    public partial class OperationClaimSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BrandDocuments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 46, 19, 39, DateTimeKind.Local).AddTicks(9335));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 46, 19, 39, DateTimeKind.Local).AddTicks(9450));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 46, 19, 39, DateTimeKind.Local).AddTicks(9451));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 46, 19, 39, DateTimeKind.Local).AddTicks(9453));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 46, 19, 39, DateTimeKind.Local).AddTicks(9528));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 46, 19, 39, DateTimeKind.Local).AddTicks(9530));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 46, 19, 39, DateTimeKind.Local).AddTicks(9560));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[] { 1, "Admin", new DateTime(2022, 11, 5, 20, 46, 19, 39, DateTimeKind.Local).AddTicks(9630), null, new DateTime(2022, 11, 5, 20, 46, 19, 39, DateTimeKind.Local).AddTicks(9631), "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "BrandDocuments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 15, 3, 112, DateTimeKind.Local).AddTicks(2557));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 15, 3, 112, DateTimeKind.Local).AddTicks(2696));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 15, 3, 112, DateTimeKind.Local).AddTicks(2698));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 15, 3, 112, DateTimeKind.Local).AddTicks(2700));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 15, 3, 112, DateTimeKind.Local).AddTicks(2789));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 15, 3, 112, DateTimeKind.Local).AddTicks(2791));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 5, 20, 15, 3, 112, DateTimeKind.Local).AddTicks(2793));
        }
    }
}
