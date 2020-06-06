using Microsoft.EntityFrameworkCore.Migrations;

namespace ShikShaq.Migrations
{
    public partial class AddFinalPriceToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "FinalPrice",
                table: "Order",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "Order");
        }
    }
}
