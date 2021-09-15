using Microsoft.EntityFrameworkCore.Migrations;

namespace GGCore.Migrations
{
    public partial class DefaultRolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44f6896f-702c-45de-b7d7-af701b93d6e5", "6f3695d1-58d4-4194-b2ee-6d23daa1a644", "Super-Admin", "SUPER-ADMIN" },
                    { "b7fb2bb9-d542-4ad7-a12e-88009903c290", "bfce86e7-336b-4dcd-88d9-e25ed27093e2", "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "44f6896f-702c-45de-b7d7-af701b93d6e5");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b7fb2bb9-d542-4ad7-a12e-88009903c290");
        }
    }
}
