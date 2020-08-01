using Microsoft.EntityFrameworkCore.Migrations;

namespace ShikShaq.Migrations
{
    public partial class DescriptionMaxUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                maxLength: 130,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 45,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Product",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 130,
                oldNullable: true);

        }
    }
}
