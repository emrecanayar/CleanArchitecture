using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rentACar.Persistence.Migrations
{
    public partial class OtpAuthenticatorV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivationKey",
                table: "OtpAuthenticators",
                newName: "SecretKey");

            migrationBuilder.AlterColumn<byte[]>(
                name: "SecretKey",
                table: "OtpAuthenticators",
                type: "varbinary(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "BrandDocuments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 9, 55, 59, DateTimeKind.Local).AddTicks(6885));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 9, 55, 59, DateTimeKind.Local).AddTicks(7047));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 9, 55, 59, DateTimeKind.Local).AddTicks(7049));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 9, 55, 59, DateTimeKind.Local).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 9, 55, 59, DateTimeKind.Local).AddTicks(7125));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 9, 55, 59, DateTimeKind.Local).AddTicks(7127));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 9, 55, 59, DateTimeKind.Local).AddTicks(7129));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecretKey",
                table: "OtpAuthenticators",
                newName: "ActivationKey");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ActivationKey",
                table: "OtpAuthenticators",
                type: "varbinary(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(256)",
                oldMaxLength: 256);

            migrationBuilder.UpdateData(
                table: "BrandDocuments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 8, 45, 163, DateTimeKind.Local).AddTicks(8689));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 8, 45, 163, DateTimeKind.Local).AddTicks(8842));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 8, 45, 163, DateTimeKind.Local).AddTicks(8843));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 8, 45, 163, DateTimeKind.Local).AddTicks(8845));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 8, 45, 163, DateTimeKind.Local).AddTicks(8920));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 8, 45, 163, DateTimeKind.Local).AddTicks(8922));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 11, 2, 14, 8, 45, 163, DateTimeKind.Local).AddTicks(8924));
        }
    }
}
