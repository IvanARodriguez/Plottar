using Microsoft.AspNetCore.JsonPatch;
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

    [HttpGet("id", Name ="GetBusiness")]
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<BusinessDto> CreateBusiness([FromBody] BusinessDto business)
    {
      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if(BusinessStore.businessList.FirstOrDefault(b => b.Name.ToLower() == business.Name.ToLower()) != null)
      {
        ModelState.AddModelError("InvalidName", "Business already exist");
        return BadRequest(ModelState);
      }
      if (business == null || business.Name == null)
      {
        return BadRequest(new { message = "Invalid Request" });
      }
      BusinessStore.businessList.Add(business);
      return CreatedAtRoute("GetBusiness", new { id=business.Id}, business);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteBusiness(string id)
    {
      if(Guid.TryParse(id, out Guid _) == false)
      {
        return BadRequest(new { message = "Invalid request" });
      }
      var business = BusinessStore.businessList.FirstOrDefault(b => b.Id == Guid.Parse(id));
      if(business == null)
      {
        return NotFound(new { message = "Business not found" });
      }
      BusinessStore.businessList.Remove(business);
      return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBusiness(string id, [FromBody] BusinessDto business)
    {
      if (Guid.TryParse(id, out Guid _) == false)
      {
        return BadRequest(new { message = "Invalid request" });
      }
      if (business == null || Guid.Parse(id) != business.Id)
      {
        return BadRequest(new { message = "Invalid request" });
      }
      var currentBusiness = BusinessStore.businessList.FirstOrDefault(b => b.Id == Guid.Parse(id));
      if (currentBusiness == null)
      {
        return NotFound(new { message = "Business not found" });
      }
      currentBusiness.Name = business.Name;
      currentBusiness.Address = business.Address;
      currentBusiness.Phone = business.Phone;
      currentBusiness.City = business.City;
      currentBusiness.State = business.State;
      currentBusiness.Country = business.Country;
      currentBusiness.PostalCode = business.PostalCode;

      return Ok(currentBusiness);
    }
    [HttpPatch("{id}")]
    public IActionResult UpdatePartialBusiness(string id,JsonPatchDocument<BusinessDto> business)
    {
      if (Guid.TryParse(id, out Guid _) == false)
      {
        return BadRequest(new { message = "Invalid request" });
      }

      var currentBusiness = BusinessStore.businessList.FirstOrDefault(b => b.Id == Guid.Parse(id));
      if (currentBusiness == null)
      {
        return NotFound(new { message = "Business not found" });
      }
      business.ApplyTo(currentBusiness, ModelState);

      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      return Ok(currentBusiness);
    }
  }
}
