using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Okane.Storage.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM public.""Categories""");
            migrationBuilder.InsertData(table: "Categories", column: "Name", value: "Food");
            migrationBuilder.InsertData(table: "Categories", column: "Name", value: "Groceries");
            migrationBuilder.InsertData(table: "Categories", column: "Name", value: "Taxes");
            migrationBuilder.InsertData(table: "Categories", column: "Name", value: "Entertainment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) => 
            migrationBuilder.Sql(@"DELETE * FROM public.""Categories""");
    }
}
