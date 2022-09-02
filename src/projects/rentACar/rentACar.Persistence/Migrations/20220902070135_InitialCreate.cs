using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rentACar.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name", "Status" },
                values: new object[] { 1, "System", new DateTime(2022, 9, 2, 10, 1, 34, 867, DateTimeKind.Local).AddTicks(3215), "", null, "BMW", 1 });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name", "Status" },
                values: new object[] { 2, "System", new DateTime(2022, 9, 2, 10, 1, 34, 867, DateTimeKind.Local).AddTicks(3218), "", null, "Mercedes", 1 });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name", "Status" },
                values: new object[] { 3, "System", new DateTime(2022, 9, 2, 10, 1, 34, 867, DateTimeKind.Local).AddTicks(3220), "", null, "Audi", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
