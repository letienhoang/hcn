using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCN.Migrations
{
    public partial class Add_More_Columns_And_Drop_TagStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HCNTagSteps");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "HCNStories",
                newName: "TopicId");

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "HCNUnits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "HCNTopics",
                type: "varchar(128)",
                unicode: false,
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "HCNTopics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "HCNTools",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "HCNToolCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "HCNTags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "HCNMaterials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "HCNMaterials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visibility",
                table: "HCNMaterialCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "HCNUnits");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "HCNTopics");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "HCNTopics");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "HCNTools");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "HCNToolCategories");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "HCNTags");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "HCNMaterials");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "HCNMaterials");

            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "HCNMaterialCategories");

            migrationBuilder.RenameColumn(
                name: "TopicId",
                table: "HCNStories",
                newName: "ParentId");

            migrationBuilder.CreateTable(
                name: "HCNTagSteps",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNTagSteps", x => new { x.TagId, x.StepId });
                });
        }
    }
}
