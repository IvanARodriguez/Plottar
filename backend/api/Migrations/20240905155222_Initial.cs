using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
  /// <inheritdoc />
  public partial class Initial : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "JobCategories",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uuid", nullable: false),
            Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_JobCategories", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Skills",
          columns: table => new
          {
            Id = table.Column<Guid>(type: "uuid", nullable: false),
            Name = table.Column<string>(type: "text", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Skills", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Jobs",
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
            table.PrimaryKey("PK_Jobs", x => x.Id);
            table.ForeignKey(
                      name: "FK_Jobs_JobCategories_JobCategoryId",
                      column: x => x.JobCategoryId,
                      principalTable: "JobCategories",
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
                      name: "FK_JobSkill_Jobs_JobsId",
                      column: x => x.JobsId,
                      principalTable: "Jobs",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_JobSkill_Skills_SkillsId",
                      column: x => x.SkillsId,
                      principalTable: "Skills",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_JobCategories_Name",
          table: "JobCategories",
          column: "Name",
          unique: true);

      migrationBuilder.CreateIndex(
          name: "IX_Jobs_JobCategoryId",
          table: "Jobs",
          column: "JobCategoryId");

      migrationBuilder.CreateIndex(
          name: "IX_JobSkill_SkillsId",
          table: "JobSkill",
          column: "SkillsId");

      migrationBuilder.CreateIndex(
          name: "IX_Skills_Name",
          table: "Skills",
          column: "Name",
          unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "JobSkill");

      migrationBuilder.DropTable(
          name: "Jobs");

      migrationBuilder.DropTable(
          name: "Skills");

      migrationBuilder.DropTable(
          name: "JobCategories");
    }
  }
}
