using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rentACar.Persistence.Migrations
{
    public partial class BrandDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandDocuments_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BrandDocuments",
                columns: new[] { "Id", "BrandId", "CreatedBy", "CreatedDate", "DocumentId", "ModifiedBy", "ModifiedDate", "Status" },
                values: new object[] { 1, 1, "System", new DateTime(2022, 10, 9, 18, 40, 49, 456, DateTimeKind.Local).AddTicks(9075), 1, "", null, 1 });

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 18, 40, 49, 456, DateTimeKind.Local).AddTicks(9248));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 18, 40, 49, 456, DateTimeKind.Local).AddTicks(9251));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 18, 40, 49, 456, DateTimeKind.Local).AddTicks(9254));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 18, 40, 49, 456, DateTimeKind.Local).AddTicks(9458));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 18, 40, 49, 456, DateTimeKind.Local).AddTicks(9462));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 10, 9, 18, 40, 49, 456, DateTimeKind.Local).AddTicks(9464));

            migrationBuilder.CreateIndex(
                name: "IX_BrandDocuments_BrandId",
                table: "BrandDocuments",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandDocuments_DocumentId",
                table: "BrandDocuments",
                column: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandDocuments");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 9, 19, 21, 17, 22, 266, DateTimeKind.Local).AddTicks(4392));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 9, 19, 21, 17, 22, 266, DateTimeKind.Local).AddTicks(4395));

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 9, 19, 21, 17, 22, 266, DateTimeKind.Local).AddTicks(4396));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2022, 9, 19, 21, 17, 22, 266, DateTimeKind.Local).AddTicks(4509));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2022, 9, 19, 21, 17, 22, 266, DateTimeKind.Local).AddTicks(4511));

            migrationBuilder.UpdateData(
                table: "Models",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2022, 9, 19, 21, 17, 22, 266, DateTimeKind.Local).AddTicks(4512));
        }
    }
}
