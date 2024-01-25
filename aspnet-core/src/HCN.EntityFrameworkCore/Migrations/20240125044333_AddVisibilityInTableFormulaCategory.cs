using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCN.Migrations
{
    public partial class AddVisibilityInTableFormulaCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "HCNFormulaCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "HCNFormulaCategories");
        }
    }
}
