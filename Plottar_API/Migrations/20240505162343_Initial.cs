using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plottar_API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "Businesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Businesses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Businesses",
                columns: new[] { "Id", "Address", "City", "Country", "CreationDate", "ImageUrl", "Name", "Phone", "PostalCode", "State", "UpdatedDate" },
                values: new object[] { new Guid("9f4c96aa-d8f6-41eb-a7b8-b98f7bdb0f5b"), "3801 Vitruvian Way", "Addison", "United States", new DateTime(2024, 5, 5, 16, 23, 43, 94, DateTimeKind.Utc).AddTicks(8841), null, "Hitab", null, "38001", "Texas", new DateTime(2024, 5, 5, 16, 23, 43, 94, DateTimeKind.Utc).AddTicks(8843) });

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_ImageUrl",
                table: "Businesses",
                column: "ImageUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_Name",
                table: "Businesses",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Businesses");
        }
    }
}
