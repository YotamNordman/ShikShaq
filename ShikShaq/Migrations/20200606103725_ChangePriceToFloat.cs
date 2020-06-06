using Microsoft.EntityFrameworkCore.Migrations;

namespace ShikShaq.Migrations
{
    public partial class ChangePriceToFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Product",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 45,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Price",
                table: "Product",
                maxLength: 45,
                nullable: true,
                oldClrType: typeof(float));
        }
    }
}
