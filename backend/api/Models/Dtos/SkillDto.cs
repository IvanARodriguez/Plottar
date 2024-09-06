namespace Api.Models.Dtos;

public class SkillDto
{
  public Guid Id { get; set; }
  public string Name { get; set; } = null!;

  public ICollection<Models.Job> Job { get; set; } = [];
}
