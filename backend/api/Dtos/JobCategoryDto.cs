// File: Dtos/JobCategoryDto.cs
namespace Api.Dtos;

public class JobCategoryDto
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
}
