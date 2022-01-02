using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinaShop.Infrastructure.EfCore.Migrations
{
    public partial class mg2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TblLanguage",
                columns: new[] { "Id", "Abbr", "Code", "IsActive", "IsRtl", "Name", "NativeName", "UseForSiteLanguage" },
                values: new object[] { new Guid("72f23210-2c23-4d51-a814-ae0601741082"), "fa", "fa-IR", true, true, "Persian", "فارسی", false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TblLanguage",
                keyColumn: "Id",
                keyValue: new Guid("72f23210-2c23-4d51-a814-ae0601741082"));
        }
    }
}
