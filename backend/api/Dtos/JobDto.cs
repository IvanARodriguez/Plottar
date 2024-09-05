namespace Api.Dtos;

public class JobDto
{
  public Guid Id { get; set; }
  public string Title { get; set; } = null!;
  public string Description { get; set; } = string.Empty;
  public decimal Salary { get; set; }
  public string SalaryType { get; set; } = null!;
  public string CurrencyCode { get; set; } = "USD";
  public string CompanyName { get; set; } = string.Empty;
  public string JobCategoryName { get; set; } = null!;
  public List<string> Skills { get; set; } = [];
}
