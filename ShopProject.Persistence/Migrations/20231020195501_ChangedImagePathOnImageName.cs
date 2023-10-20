using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangedImagePathOnImageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "ProductImages",
                newName: "ImageName");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProductImages");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "ProductImages",
                newName: "ImagePath");
        }
    }
}
