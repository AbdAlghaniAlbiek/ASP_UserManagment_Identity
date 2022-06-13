using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagementWebIdentity.Data.Migrations
{
    public partial class AddRolesToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [security].[UserRoles] (UserId, RoleId) SELECT 'c74ef253-a438-4892-975f-83e28f7eed4c', Id FROM [security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [security].[UserRoles] WHERE UserId = 'c74ef253-a438-4892-975f-83e28f7eed4c' ");
        }
    }
}
