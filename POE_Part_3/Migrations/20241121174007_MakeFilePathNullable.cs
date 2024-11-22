using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POE_Part_3.Migrations
{
    /// <inheritdoc />
    public partial class MakeFilePathNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Alter the FilePath column to be nullable
            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Claims",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert the FilePath column to be non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "FilePath",
                table: "Claims",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}


