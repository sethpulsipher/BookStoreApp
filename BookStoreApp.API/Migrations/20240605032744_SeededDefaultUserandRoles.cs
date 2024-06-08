using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserandRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1427B7E9-F449-4348-9643-39B7A45312F1", null, "Administrator", "ADMINISTRATOR" },
                    { "36BD093A-0E70-4B9D-8B6A-32C194B8329D", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2BCB2E94-A038-4684-AF85-F8A0B253FEED", 0, "97a9eecf-8ae8-468f-a5a2-9336e5a3bc56", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEF22NDtHOJ1fBKZir4ok/9qQMu9loE8zZinhdYfIGyTUie6D2G/va+YtnEFkWyR0MA==", null, false, "c3f41214-45ee-4c3b-977f-b6b9e5b799e9", false, "user@bookstore.com" },
                    { "E83BB8A6-DEAA-4843-9465-7B25ACBCB423", 0, "2291ae29-ef3f-4695-a734-6a64e7844d2d", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEBxtpUTW+lt02tx40Mkd27Yt0UU6wlUYnAEDxilHwlO8CgQbRs/0ghTnwx3gTFJtaw==", null, false, "1f9b3404-27c0-4aae-b31d-7f75607d34d0", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "36BD093A-0E70-4B9D-8B6A-32C194B8329D", "2BCB2E94-A038-4684-AF85-F8A0B253FEED" },
                    { "1427B7E9-F449-4348-9643-39B7A45312F1", "E83BB8A6-DEAA-4843-9465-7B25ACBCB423" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "36BD093A-0E70-4B9D-8B6A-32C194B8329D", "2BCB2E94-A038-4684-AF85-F8A0B253FEED" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1427B7E9-F449-4348-9643-39B7A45312F1", "E83BB8A6-DEAA-4843-9465-7B25ACBCB423" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1427B7E9-F449-4348-9643-39B7A45312F1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36BD093A-0E70-4B9D-8B6A-32C194B8329D");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2BCB2E94-A038-4684-AF85-F8A0B253FEED");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "E83BB8A6-DEAA-4843-9465-7B25ACBCB423");
        }
    }
}
