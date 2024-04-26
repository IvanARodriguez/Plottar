using Plottar_API.Models.Dto;

namespace Plottar_API.Data
{
  public static class BusinessStore
  {
    public static List<BusinessDto> businessList =
    [
      new() 
      {
        Name="Don Luxury", 
        Address="3801 Vitruvian Way", 
        City="Addison", 
        State="TX", 
        Country="US",
        PostalCode="75001"
      },
      new() 
      {
        Name="Maravilla",
        Address="4758 Medow Point",
        City="Tampa",
        State="FL",
        Country="US",
        PostalCode="33545"
      },
    ];
  }
}
