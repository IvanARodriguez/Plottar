using AutoMapper;
using Plottar_API.Models;
using Plottar_API.Models.Dto;

namespace Plottar_API
{
  public class MappingConfig : Profile
  {
    public MappingConfig() 
    { 
      CreateMap<Business, BusinessDto>().ReverseMap();
      CreateMap<Business, BusinessCreateDto>().ReverseMap();
      CreateMap<Business, BusinessUpdateDto>().ReverseMap();
    }
  }
}
