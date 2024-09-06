namespace Api.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Constants;

public class Job
{
  public Guid Id { get; set; }

  [Required, MaxLength(100)]
  public string Title { get; set; } = null!;

  [Required]
  public string Description { get; set; } = string.Empty;

  [MaxLength(255)]
  public string ShortDescription { get; set; } = string.Empty;

  [MaxLength(100)]
  public string CompanyName { get; set; } = string.Empty;

  public DateTime CreateDate { get; set; } = DateTime.UtcNow;
  public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

  [Column(TypeName = "decimal(18,2)")]
  public decimal Salary { get; set; }

  [Required]
  public SalaryType SalaryType { get; set; } = SalaryType.Year;

  [Required, MaxLength(3)]
  public string CurrencyCode { get; set; } = "USD";

  [Required]
  public JobUserType JobUserType { get; set; } = JobUserType.RegisteredUser;

  public Guid? UserId { get; set; }

  [MaxLength(100)]
  public string? AnonymousUserName { get; set; }

  [Required]
  public JobStatus Status { get; set; } = JobStatus.Active;

  [Required]
  public Guid JobCategoryId { get; set; }

  public JobCategory? Category { get; set; }

  public List<Skill> Skills { get; set; } = [];
}
