using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rentACar.Persistence.Migrations
{
    public partial class OperationClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOperationClaim_OperationClaim_OperationClaimId",
                table: "UserOperationClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationClaim",
                table: "OperationClaim");

            migrationBuilder.RenameTable(
                name: "OperationClaim",
                newName: "OperationClaims");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "OperationClaims",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OperationClaims",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "OperationClaims",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "OperationClaims",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "OperationClaims",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationClaims",
                table: "OperationClaims",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "BrandDocuments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 31, 22, 343, DateTimeKind.Local).AddTicks(726));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 31, 22, 343, DateTimeKind.Local).AddTicks(891));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 31, 22, 343, DateTimeKind.Local).AddTicks(893));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 31, 22, 343, DateTimeKind.Local).AddTicks(895));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 31, 22, 343, DateTimeKind.Local).AddTicks(972));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 31, 22, 343, DateTimeKind.Local).AddTicks(974));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 31, 22, 343, DateTimeKind.Local).AddTicks(976));

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperationClaim_OperationClaims_OperationClaimId",
                table: "UserOperationClaim",
                column: "OperationClaimId",
                principalTable: "OperationClaims",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOperationClaim_OperationClaims_OperationClaimId",
                table: "UserOperationClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OperationClaims",
                table: "OperationClaims");

            migrationBuilder.RenameTable(
                name: "OperationClaims",
                newName: "OperationClaim");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "OperationClaim",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OperationClaim",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "OperationClaim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "OperationClaim",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "OperationClaim",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OperationClaim",
                table: "OperationClaim",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "BrandDocuments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 23, 35, 747, DateTimeKind.Local).AddTicks(1080));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 23, 35, 747, DateTimeKind.Local).AddTicks(1199));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 23, 35, 747, DateTimeKind.Local).AddTicks(1201));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 23, 35, 747, DateTimeKind.Local).AddTicks(1203));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 23, 35, 747, DateTimeKind.Local).AddTicks(1267));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 23, 35, 747, DateTimeKind.Local).AddTicks(1269));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 19, 23, 35, 747, DateTimeKind.Local).AddTicks(1270));

            migrationBuilder.AddForeignKey(
                name: "FK_UserOperationClaim_OperationClaim_OperationClaimId",
                table: "UserOperationClaim",
                column: "OperationClaimId",
                principalTable: "OperationClaim",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
