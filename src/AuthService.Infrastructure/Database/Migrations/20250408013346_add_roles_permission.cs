using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthService.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class add_roles_permission : Migration
{
    // Define static readonly fields for constant arrays
    private static readonly string[] PermissionColumns = { "id", "name" };
    private static readonly string[] RoleColumns = { "id", "name" };
    private static readonly string[] RolePermissionColumns = { "permission_id", "role_id" };

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "permissions",
            schema: "auth",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_permissions", x => x.id));

        migrationBuilder.CreateTable(
            name: "roles",
            schema: "auth",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => table.PrimaryKey("pk_roles", x => x.id));

        migrationBuilder.CreateTable(
            name: "role_permission",
            schema: "auth",
            columns: table => new
            {
                role_id = table.Column<int>(type: "integer", nullable: false),
                permission_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_role_permission", x => new { x.role_id, x.permission_id });
                table.ForeignKey(
                    name: "fk_role_permission_permissions_permission_id",
                    column: x => x.permission_id,
                    principalSchema: "auth",
                    principalTable: "permissions",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_role_permission_roles_role_id",
                    column: x => x.role_id,
                    principalSchema: "auth",
                    principalTable: "roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "role_user",
            schema: "auth",
            columns: table => new
            {
                role_id = table.Column<int>(type: "integer", nullable: false),
                users_id = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_role_user", x => new { x.role_id, x.users_id });
                table.ForeignKey(
                    name: "fk_role_user_roles_role_id",
                    column: x => x.role_id,
                    principalSchema: "auth",
                    principalTable: "roles",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_role_user_user_users_id",
                    column: x => x.users_id,
                    principalSchema: "auth",
                    principalTable: "users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "auth",
            table: "permissions",
            columns: PermissionColumns,
            values: new object[,]
            {
                    { 1, "Create_User" },
                    { 2, "Read_User" },
                    { 3, "Update_User" },
                    { 4, "Delete_User" }
            });

        migrationBuilder.InsertData(
            schema: "auth",
            table: "roles",
            columns: RoleColumns,
            values: new object[] { 1, "Admin" });

        migrationBuilder.InsertData(
            schema: "auth",
            table: "role_permission",
            columns: RolePermissionColumns,
            values: new object[,]
            {
                    { 1, 1 },
                    { 2, 1 }
            });

        migrationBuilder.CreateIndex(
            name: "ix_role_permission_permission_id",
            schema: "auth",
            table: "role_permission",
            column: "permission_id");

        migrationBuilder.CreateIndex(
            name: "ix_role_user_users_id",
            schema: "auth",
            table: "role_user",
            column: "users_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "role_permission",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "role_user",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "permissions",
            schema: "auth");

        migrationBuilder.DropTable(
            name: "roles",
            schema: "auth");
    }
}
