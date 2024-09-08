using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddJobSkillRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId1",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_Jobs_JobsId",
                table: "JobSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_Skills_SkillsId",
                table: "JobSkill");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobCategoryId1",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobCategoryId1",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "SkillsId",
                table: "JobSkill",
                newName: "SkillId");

            migrationBuilder.RenameColumn(
                name: "JobsId",
                table: "JobSkill",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobSkill_SkillsId",
                table: "JobSkill",
                newName: "IX_JobSkill_SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs",
                column: "JobCategoryId",
                principalTable: "JobCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_Jobs_JobId",
                table: "JobSkill",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_Skills_SkillId",
                table: "JobSkill",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_Jobs_JobId",
                table: "JobSkill");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSkill_Skills_SkillId",
                table: "JobSkill");

            migrationBuilder.RenameColumn(
                name: "SkillId",
                table: "JobSkill",
                newName: "SkillsId");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "JobSkill",
                newName: "JobsId");

            migrationBuilder.RenameIndex(
                name: "IX_JobSkill_SkillId",
                table: "JobSkill",
                newName: "IX_JobSkill_SkillsId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_Jobs_JobsId",
                table: "JobSkill",
                column: "JobsId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSkill_Skills_SkillsId",
                table: "JobSkill",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
