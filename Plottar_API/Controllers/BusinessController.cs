using Microsoft.AspNetCore.Mvc;
using Plottar_API.Data;
using Plottar_API.Models.Dto;

namespace Plottar_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BusinessController(ILogger<BusinessController> logger) : ControllerBase
  {
    private readonly ILogger<BusinessController> _logger = logger;

    [HttpGet]
    public ActionResult<IEnumerable<BusinessDto>> GetBusinesses()
    {
      _logger.LogInformation("Get Route has been hit");
      return Ok(BusinessStore.businessList);
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BusinessDto> GetBusiness(string id)
    {
      bool isValidGuid = Guid.TryParse(id, out _);

      if (String.IsNullOrEmpty(id) || !isValidGuid)
      {
        return BadRequest(new { message =  "Invalid Request"});
      }
      var business = BusinessStore.businessList.FirstOrDefault(b => b.Id == Guid.Parse(id));

      if (business == null)
      {
        return NotFound(new { message = "Business not found" });
      }

      return Ok(business);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<BusinessDto> CreateBusiness([FromBody] BusinessDto business)
    {
      if (business == null || business.Name == null)
      {
        return BadRequest(new { message = "Invalid Request" });
      }
      BusinessStore.businessList.Add(business);
      return Ok(business);
    }
  }
}
