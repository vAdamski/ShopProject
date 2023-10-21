using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "ProductImages",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "ProductImages",
                newName: "ImageName");
        }
    }
}
