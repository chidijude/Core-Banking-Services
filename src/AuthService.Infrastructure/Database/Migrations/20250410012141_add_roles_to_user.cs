using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class add_roles_to_user : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_role_user_roles_role_id",
            schema: "auth",
            table: "role_user");

        migrationBuilder.DropForeignKey(
            name: "fk_role_user_user_users_id",
            schema: "auth",
            table: "role_user");

        migrationBuilder.RenameColumn(
            name: "users_id",
            schema: "auth",
            table: "role_user",
            newName: "user_id");

        migrationBuilder.RenameColumn(
            name: "role_id",
            schema: "auth",
            table: "role_user",
            newName: "roles_id");

        migrationBuilder.RenameIndex(
            name: "ix_role_user_users_id",
            schema: "auth",
            table: "role_user",
            newName: "ix_role_user_user_id");

        migrationBuilder.AddForeignKey(
            name: "fk_role_user_roles_roles_id",
            schema: "auth",
            table: "role_user",
            column: "roles_id",
            principalSchema: "auth",
            principalTable: "roles",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_role_user_users_user_id",
            schema: "auth",
            table: "role_user",
            column: "user_id",
            principalSchema: "auth",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "fk_role_user_roles_roles_id",
            schema: "auth",
            table: "role_user");

        migrationBuilder.DropForeignKey(
            name: "fk_role_user_users_user_id",
            schema: "auth",
            table: "role_user");

        migrationBuilder.RenameColumn(
            name: "user_id",
            schema: "auth",
            table: "role_user",
            newName: "users_id");

        migrationBuilder.RenameColumn(
            name: "roles_id",
            schema: "auth",
            table: "role_user",
            newName: "role_id");

        migrationBuilder.RenameIndex(
            name: "ix_role_user_user_id",
            schema: "auth",
            table: "role_user",
            newName: "ix_role_user_users_id");

        migrationBuilder.AddForeignKey(
            name: "fk_role_user_roles_role_id",
            schema: "auth",
            table: "role_user",
            column: "role_id",
            principalSchema: "auth",
            principalTable: "roles",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "fk_role_user_user_users_id",
            schema: "auth",
            table: "role_user",
            column: "users_id",
            principalSchema: "auth",
            principalTable: "users",
            principalColumn: "id",
            onDelete: ReferentialAction.Cascade);
    }
}
