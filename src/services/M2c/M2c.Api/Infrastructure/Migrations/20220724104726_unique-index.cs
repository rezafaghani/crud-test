using Microsoft.EntityFrameworkCore.Migrations;

namespace M2c.Api.Infrastructure.Migrations
{
    public partial class uniqueindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_Firstname_Lastname_DateOfBirth",
                schema: "M2C",
                table: "Customers",
                columns: new[] { "Firstname", "Lastname", "DateOfBirth" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_Firstname_Lastname_DateOfBirth",
                schema: "M2C",
                table: "Customers");
        }
    }
}
