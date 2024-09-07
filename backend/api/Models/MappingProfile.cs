

namespace Api.Models;

using Api.Models.Dtos;
using Api.Models.Dtos.Job;
using AutoMapper;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    this.CreateMap<Job, JobDto>()
        .ForMember(dest => dest.JobCategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""))
        .ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Select(s => s.Name).ToList()))
        .ReverseMap();

    this.CreateMap<JobCategory, JobCategoryDto>()
        .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.Jobs.Select(j => j.Id).ToList()));

    this.CreateMap<CreateJobDto, Job>()
        .ForMember(dest => dest.Category, opt => opt.Ignore())
        .ForMember(dest => dest.Skills, opt => opt.Ignore());

    this.CreateMap<UpdateJobRequestDto, Job>()
        .ForMember(dest => dest.Category, opt => opt.Ignore())
        .ForMember(dest => dest.Skills, opt => opt.Ignore());
  }
}
