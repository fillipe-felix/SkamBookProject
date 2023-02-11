using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkamBook.Infrastructure.Migrations.Skambook
{
    public partial class UpdateRelationshipTableMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookIdLike",
                table: "Matches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookIdLike",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
