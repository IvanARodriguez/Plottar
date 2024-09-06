// File: Dtos/JobCategoryDto.cs
namespace Api.Models.Dtos;

public class JobCategoryDto
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;
  public ICollection<Models.Job> Jobs { get; set; } = [];
}
