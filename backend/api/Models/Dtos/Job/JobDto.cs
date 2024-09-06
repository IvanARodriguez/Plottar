namespace Api.Models.Dtos.Job;


public class JobDto
{
  public Guid Id { get; set; }
  public string Title { get; set; } = null!;
  public string Description { get; set; } = string.Empty;
  public string ShortDescription { get; set; } = string.Empty;
  public decimal Salary { get; set; }
  public string Status { get; set; } = string.Empty;
  public string SalaryType { get; set; } = null!;
  public string JobUserType { get; set; } = null!;
  public string CurrencyCode { get; set; } = "USD";
  public string? AnonymousUserName { get; set; }
  public string CompanyName { get; set; } = string.Empty;
  public string JobCategoryName { get; set; } = null!;
  public Guid JobCategoryId { get; set; }
  public List<string> Skills { get; set; } = [];
}
