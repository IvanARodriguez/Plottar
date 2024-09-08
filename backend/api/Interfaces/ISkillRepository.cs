namespace Api.Interfaces;

using Api.Models.Dtos.Skills;
using ErrorOr;

public interface ISkillRepository
{
  Task<IEnumerable<SkillDto>> GetSkillsAsync();
  Task<ErrorOr<SkillDto?>> GetSkillByIdAsync(Guid id);
  Task<ErrorOr<SkillDto>> CreateSkillAsync(CreateSkillDto createSkillDto);
  Task<ErrorOr<SkillDto>> DeleteSkillAsync(Guid id);
  Task<ErrorOr<SkillDto>> UpdateSkillAsync(Guid id, SkillDto skillDto);
}
