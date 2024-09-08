

namespace Api.Models;

using Api.Models.Dtos;
using Api.Models.Dtos.Job;
using Api.Models.Dtos.Skills;
using AutoMapper;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    // Map Jobs
    CreateMap<Job, JobDto>()
     .ForMember(dest =>
      dest.JobCategoryName,
      opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""))
     .ForMember(dest =>
       dest.Skills,
       opt => opt.MapFrom(src => src.JobSkills.Select(js => js.Skill.Name).ToList()))
     .ReverseMap();

    CreateMap<JobCategory, JobCategoryDto>()
        .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.Jobs.Select(j => j.Id).ToList()));

    CreateMap<CreateJobDto, Job>()
        .ForMember(dest => dest.Category, opt => opt.Ignore())
        .ForMember(dest => dest.JobSkills, opt => opt.Ignore());

    CreateMap<UpdateJobRequestDto, Job>()
        .ForMember(dest => dest.Category, opt => opt.Ignore());

    // Map Skills
    CreateMap<Skill, SkillDto>().ReverseMap();
    CreateMap<CreateSkillDto, Skill>();
  }
}
