using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HCN.Migrations
{
    public partial class Add_ParentId_For_Formuala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "HCNFormulas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "HCNFormulas");
        }
    }
}
