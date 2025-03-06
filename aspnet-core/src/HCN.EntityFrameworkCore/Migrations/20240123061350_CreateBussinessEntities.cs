using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCN.Migrations
{
    public partial class CreateBussinessEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HCNFormulaCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    CoverPicture = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeywordSEO = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DescriptionSEO = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNFormulaCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNFormulaMaterials",
                columns: table => new
                {
                    FormulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNFormulaMaterials", x => new { x.FormulaId, x.MaterialId });
                });

            migrationBuilder.CreateTable(
                name: "HCNFormulas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ExecutionTime = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ThumbnailPicture = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    BriefContent = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Liked = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Visibility = table.Column<bool>(type: "bit", nullable: false),
                    VideoUrl = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    ReferenceSource = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    KeywordSEO = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DescriptionSEO = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNFormulas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNFormulaSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Pictures = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNFormulaSteps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNFormulaTools",
                columns: table => new
                {
                    FormulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNFormulaTools", x => new { x.FormulaId, x.ToolId });
                });

            migrationBuilder.CreateTable(
                name: "HCNMaterialCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    CoverPicture = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeywordSEO = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DescriptionSEO = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNMaterialCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNMaterials",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pictures = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    KeywordSEO = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DescriptionSEO = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNMaterials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Star = table.Column<int>(type: "int", nullable: true),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewType = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expense = table.Column<decimal>(type: "decimal(19,4)", nullable: false),
                    Liked = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Pictures = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Visibility = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNReviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNStories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    BriefContent = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailPicture = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Pictures = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Liked = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ViewCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    Visibility = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceSource = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    KeywordSEO = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DescriptionSEO = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNStories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNTagFormulas",
                columns: table => new
                {
                    FormulaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNTagFormulas", x => new { x.TagId, x.FormulaId });
                });

            migrationBuilder.CreateTable(
                name: "HCNTagMaterials",
                columns: table => new
                {
                    MaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNTagMaterials", x => new { x.TagId, x.MaterialId });
                });

            migrationBuilder.CreateTable(
                name: "HCNTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNTagSteps",
                columns: table => new
                {
                    StepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNTagSteps", x => new { x.TagId, x.StepId });
                });

            migrationBuilder.CreateTable(
                name: "HCNTagStories",
                columns: table => new
                {
                    StoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNTagStories", x => new { x.TagId, x.StoryId });
                });

            migrationBuilder.CreateTable(
                name: "HCNTagTools",
                columns: table => new
                {
                    ToolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNTagTools", x => new { x.TagId, x.ToolId });
                });

            migrationBuilder.CreateTable(
                name: "HCNToolCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    CoverPicture = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeywordSEO = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DescriptionSEO = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNToolCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNTools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToolType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pictures = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    KeywordSEO = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DescriptionSEO = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNTools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNTopics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Slug = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    CoverPicture = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeywordSEO = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DescriptionSEO = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNTopics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HCNUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    BriefContent = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HCNUnits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HCNFormulaCategories");

            migrationBuilder.DropTable(
                name: "HCNFormulaMaterials");

            migrationBuilder.DropTable(
                name: "HCNFormulas");

            migrationBuilder.DropTable(
                name: "HCNFormulaSteps");

            migrationBuilder.DropTable(
                name: "HCNFormulaTools");

            migrationBuilder.DropTable(
                name: "HCNMaterialCategories");

            migrationBuilder.DropTable(
                name: "HCNMaterials");

            migrationBuilder.DropTable(
                name: "HCNReviews");

            migrationBuilder.DropTable(
                name: "HCNStories");

            migrationBuilder.DropTable(
                name: "HCNTagFormulas");

            migrationBuilder.DropTable(
                name: "HCNTagMaterials");

            migrationBuilder.DropTable(
                name: "HCNTags");

            migrationBuilder.DropTable(
                name: "HCNTagSteps");

            migrationBuilder.DropTable(
                name: "HCNTagStories");

            migrationBuilder.DropTable(
                name: "HCNTagTools");

            migrationBuilder.DropTable(
                name: "HCNToolCategories");

            migrationBuilder.DropTable(
                name: "HCNTools");

            migrationBuilder.DropTable(
                name: "HCNTopics");

            migrationBuilder.DropTable(
                name: "HCNUnits");
        }
    }
}
