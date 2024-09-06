using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJobModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs");

            migrationBuilder.AddColumn<Guid>(
                name: "JobCategoryId1",
                table: "Jobs",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobCategoryId1",
                table: "Jobs",
                column: "JobCategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs",
                column: "JobCategoryId",
                principalTable: "JobCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId1",
                table: "Jobs",
                column: "JobCategoryId1",
                principalTable: "JobCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId1",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobCategoryId1",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobCategoryId1",
                table: "Jobs");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs",
                column: "JobCategoryId",
                principalTable: "JobCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
