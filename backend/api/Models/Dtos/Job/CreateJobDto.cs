namespace Api.Models.Dtos.Job;

public class CreateJobDto
{
  public string Title { get; set; } = null!;
  public string Description { get; set; } = null!;
  public string ShortDescription { get; set; } = null!;
  public string CompanyName { get; set; } = null!;
  public decimal Salary { get; set; }
  public string CurrencyCode { get; set; } = null!;
  public Guid? UserId { get; set; }
  public string? AnonymousUserName { get; set; }
  public Guid JobCategoryId { get; set; }
  public List<string> Skills { get; set; } = [];
  public string SalaryType { get; set; } = null!;
  public string JobUserType { get; set; } = null!;
  public string Status { get; set; } = null!;
}
