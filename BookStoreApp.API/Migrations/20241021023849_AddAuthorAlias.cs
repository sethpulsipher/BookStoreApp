using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorAlias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2BCB2E94-A038-4684-AF85-F8A0B253FEED",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "291fbaf0-4704-4d2b-af8e-a7829ea89342", "AQAAAAIAAYagAAAAEJPIGUuErWboO3Yg/QKfY2lz/ItD/PKijo39gkJcWTJyrfCoRUdimiwOayG24JhlQg==", "025229fd-ede5-42ca-a1ec-a6081567b2bc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "E83BB8A6-DEAA-4843-9465-7B25ACBCB423",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5dd68cc1-10a6-4036-b48a-2930b169ebd7", "AQAAAAIAAYagAAAAEPp9ON3J3lvYO40sTLrYxPdQGElLBI11AaIqNhjbK20F6HosKM51K/4nqZ83cqMEfA==", "38ff8f0a-fb41-450f-9db5-5a4da9cd84e7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Authors");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2BCB2E94-A038-4684-AF85-F8A0B253FEED",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97a9eecf-8ae8-468f-a5a2-9336e5a3bc56", "AQAAAAIAAYagAAAAEF22NDtHOJ1fBKZir4ok/9qQMu9loE8zZinhdYfIGyTUie6D2G/va+YtnEFkWyR0MA==", "c3f41214-45ee-4c3b-977f-b6b9e5b799e9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "E83BB8A6-DEAA-4843-9465-7B25ACBCB423",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2291ae29-ef3f-4695-a734-6a64e7844d2d", "AQAAAAIAAYagAAAAEBxtpUTW+lt02tx40Mkd27Yt0UU6wlUYnAEDxilHwlO8CgQbRs/0ghTnwx3gTFJtaw==", "1f9b3404-27c0-4aae-b31d-7f75607d34d0" });
        }
    }
}
