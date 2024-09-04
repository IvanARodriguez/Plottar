using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
  /// <inheritdoc />
  public partial class initial : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "JobCategory",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uuid", nullable: false),
            Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_JobCategory", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Skill",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uuid", nullable: false),
            Name = table.Column<string>(type: "text", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Skill", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Job",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uuid", nullable: false),
            Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            Description = table.Column<string>(type: "text", nullable: false),
            ShortDescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
            CompanyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            Salary = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
            SalaryType = table.Column<int>(type: "integer", nullable: false),
            CurrencyCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
            JobUserType = table.Column<int>(type: "integer", nullable: false),
            UserId = table.Column<Guid>(type: "uuid", nullable: true),
            AnonymousUserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
            Status = table.Column<int>(type: "integer", nullable: false),
            JobCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Job", x => x.Id);
            table.ForeignKey(
                      name: "FK_Job_JobCategory_JobCategoryId",
                      column: x => x.JobCategoryId,
                      principalTable: "JobCategory",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "JobSkill",
          columns: table => new
          {
            JobsId = table.Column<Guid>(type: "uuid", nullable: false),
            SkillsId = table.Column<Guid>(type: "uuid", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_JobSkill", x => new { x.JobsId, x.SkillsId });
            table.ForeignKey(
                      name: "FK_JobSkill_Job_JobsId",
                      column: x => x.JobsId,
                      principalTable: "Job",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_JobSkill_Skill_SkillsId",
                      column: x => x.SkillsId,
                      principalTable: "Skill",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Job_JobCategoryId",
          table: "Job",
          column: "JobCategoryId");

      migrationBuilder.CreateIndex(
          name: "IX_JobCategory_Name",
          table: "JobCategory",
          column: "Name",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_JobSkill_SkillsId",
          table: "JobSkill",
          column: "SkillsId");

      migrationBuilder.CreateIndex(
          name: "IX_Skill_Name",
          table: "Skill",
          column: "Name",
          unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "JobSkill");

      migrationBuilder.DropTable(
          name: "Job");

      migrationBuilder.DropTable(
          name: "Skill");

      migrationBuilder.DropTable(
          name: "JobCategory");
    }
  }
}
