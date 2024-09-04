namespace Api.Models;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Api.Constants;

public class Job
{
  public Guid Id { get; set; }

  [Required, MaxLength(100)]
  public required string Title { get; set; } = null!;

  [Required]
  public required string Description { get; set; }

  [MaxLength(255)]
  public string ShortDescription { get; set; } = string.Empty;

  [MaxLength(100)]
  public string CompanyName { get; set; } = string.Empty;

  public DateOnly CreateDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
  public DateOnly UpdateDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

  [Column(TypeName = "decimal(18,2)")]
  public decimal Salary { get; set; }

  [Required]
  public required string SalaryType { get; set; } = "Annual"; // Consider Enum

  [Required, MaxLength(3)]
  public string CurrencyCode { get; set; } = "USD"; // Consider Enum or constrained list

  public Guid? UserId { get; set; } // Nullable if anonymous

  [MaxLength(100)]
  public string AnonymousUserName { get; set; } = string.Empty; // Optional

  [Required]
  public required JobStatus Status { get; set; } = JobStatus.Active; // Consider Enum
}
