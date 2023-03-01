using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAspNetCoreApp2.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddExpire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsPublish",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Expire",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expire",
                table: "Products");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublish",
                table: "Products",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
