using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinaShop.Infrastructure.EfCore.Migrations
{
    public partial class mg3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblLanguage");

            migrationBuilder.CreateTable(
                name: "TblLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Abbr = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NativeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsRtl = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UseForSiteLanguage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblLanguages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblLanguages");

            migrationBuilder.CreateTable(
                name: "TblLanguage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 450, nullable: false),
                    Abbr = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsRtl = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NativeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UseForSiteLanguage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblLanguage", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TblLanguage",
                columns: new[] { "Id", "Abbr", "Code", "IsActive", "IsRtl", "Name", "NativeName", "UseForSiteLanguage" },
                values: new object[] { new Guid("72f23210-2c23-4d51-a814-ae0601741082"), "fa", "fa-IR", true, true, "Persian", "فارسی", false });
        }
    }
}
