using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkamBook.Infrastructure.Migrations.Skambook
{
    public partial class UpdateNamePropertiesTableMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User2Id",
                table: "Matches",
                newName: "UserIdLiked");

            migrationBuilder.RenameColumn(
                name: "User1Id",
                table: "Matches",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Book2Id",
                table: "Matches",
                newName: "BookIdLiked");

            migrationBuilder.RenameColumn(
                name: "Book1Id",
                table: "Matches",
                newName: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserIdLiked",
                table: "Matches",
                newName: "User2Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Matches",
                newName: "User1Id");

            migrationBuilder.RenameColumn(
                name: "BookIdLiked",
                table: "Matches",
                newName: "Book2Id");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Matches",
                newName: "Book1Id");
        }
    }
}
