namespace Api.Models.Relationship;

public class JobSkill
{
  public Guid JobId { get; set; }
  public Job Job { get; set; } = null!;

  public Guid SkillId { get; set; }
  public Skill Skill { get; set; } = null!;
}
