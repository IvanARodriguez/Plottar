using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plottar_API.Migrations
{
    /// <inheritdoc />
    public partial class Update_Business_Phone_Length : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: new Guid("9f4c96aa-d8f6-41eb-a7b8-b98f7bdb0f5b"));

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Businesses",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Businesses",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "Address", "City", "Country", "CreationDate", "ImageUrl", "Name", "Phone", "PostalCode", "State", "UpdatedDate" },
                values: new object[] { new Guid("32754ab1-20e1-4c42-a4b0-7066f5c0db20"), "3801 Vitruvian Way", "Addison", "United States", new DateTime(2024, 5, 5, 17, 12, 40, 579, DateTimeKind.Utc).AddTicks(6225), null, "Hitab", null, "38001", "Texas", new DateTime(2024, 5, 5, 17, 12, 40, 579, DateTimeKind.Utc).AddTicks(6227) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Businesses",
                keyColumn: "Id",
                keyValue: new Guid("32754ab1-20e1-4c42-a4b0-7066f5c0db20"));

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "Businesses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Businesses",
                type: "character varying(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "Address", "City", "Country", "CreationDate", "ImageUrl", "Name", "Phone", "PostalCode", "State", "UpdatedDate" },
                values: new object[] { new Guid("9f4c96aa-d8f6-41eb-a7b8-b98f7bdb0f5b"), "3801 Vitruvian Way", "Addison", "United States", new DateTime(2024, 5, 5, 16, 23, 43, 94, DateTimeKind.Utc).AddTicks(8841), null, "Hitab", null, "38001", "Texas", new DateTime(2024, 5, 5, 16, 23, 43, 94, DateTimeKind.Utc).AddTicks(8843) });
        }
    }
}
