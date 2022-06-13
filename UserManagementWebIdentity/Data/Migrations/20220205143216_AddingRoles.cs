using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace UserManagementWebIdentity.Data.Migrations
{
    public partial class AddingRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Role1", "role1".ToUpper(), Guid.NewGuid().ToString() },
                schema: "security"
                );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Role2", "role2".ToUpper(), Guid.NewGuid().ToString() },
                schema: "security"
                );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Role3", "role3".ToUpper(), Guid.NewGuid().ToString() },
                schema: "security"
                );

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Role4", "role4".ToUpper(), Guid.NewGuid().ToString() },
                schema: "security"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
