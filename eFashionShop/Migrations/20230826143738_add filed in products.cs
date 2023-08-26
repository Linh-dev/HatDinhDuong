using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eFashionShop.Migrations
{
    public partial class addfiledinproducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Customer",
                table: "Products",
                newName: "Trademark");

            migrationBuilder.AddColumn<string>(
                name: "BlogDescription",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Expiry",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ingredient",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductOrigin",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "64b047b8-af27-4449-b48a-6839065c4559");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "89348e07-acdf-4170-8adc-96c1dcdf6c8e", "AQAAAAEAACcQAAAAEPCIgIb/2n2t+pOzw/yqEHqIo6VTAGQKem9ZY4jIWXh4ODG/Sh50/hVy+rh9UYJz4w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogDescription",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Expiry",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Ingredient",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductOrigin",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Trademark",
                table: "Products",
                newName: "Customer");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "4eab5a58-5cdd-4f20-b777-75bd7fa09a02");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "18f57887-c727-4993-a03e-e6d896ba9af2", "AQAAAAEAACcQAAAAENeNI0SKmF/4F15d9DP3oKrFThm311q0LoB0CyKgaygo+aXlKQJflL4X1qnLjRmkNw==" });
        }
    }
}
