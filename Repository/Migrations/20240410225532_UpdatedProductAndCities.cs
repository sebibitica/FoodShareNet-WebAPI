using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodShareNet.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProductAndCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Arad" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Image",
                value: "https://www.tastingtable.com/img/gallery/20-tricks-to-make-your-tomatoes-even-more-delicious/intro-1684770527.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Image",
                value: "https://www.foodrepublic.com/img/gallery/10-things-you-probably-didnt-know-about-potatoes/l-intro-1689785695.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "Image",
                value: "https://staticcookist.akamaized.net/wp-content/uploads/sites/22/2020/06/meat-1200x675.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Products");
        }
    }
}
