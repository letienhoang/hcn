using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCN.Migrations
{
    public partial class AddMaterialType_And_ThumbnailPicture_For_Material_And_Tool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPicture",
                table: "HCNTools",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaterialType",
                table: "HCNMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ThumbnailPicture",
                table: "HCNMaterials",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailPicture",
                table: "HCNTools");

            migrationBuilder.DropColumn(
                name: "MaterialType",
                table: "HCNMaterials");

            migrationBuilder.DropColumn(
                name: "ThumbnailPicture",
                table: "HCNMaterials");
        }
    }
}
