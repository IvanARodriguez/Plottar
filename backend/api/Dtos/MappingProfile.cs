

namespace Api.Dtos;

using Api.Models;
using AutoMapper;

public class MappingProfile : Profile
{
  public MappingProfile() => this.CreateMap<Job, JobDto>()
        .ForMember(dest =>
          dest.JobCategoryName,
          opt =>
            opt.MapFrom(src => src.Category.Name))
        .ForMember(dest =>
          dest.Skills, opt =>
            opt.MapFrom(src =>
              src.Skills
              .Select(s => s.Name)
              .ToList()));
}
