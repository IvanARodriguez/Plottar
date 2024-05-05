using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plottar_API.Data;
using Plottar_API.Models;
using Plottar_API.Models.Dto;

namespace Plottar_API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BusinessController(ILogger<BusinessController> Logger, ApplicationDBContext dBContext) : ControllerBase
  {
    private readonly ILogger<BusinessController> _logger = Logger;
    private readonly ApplicationDBContext _dBContext = dBContext;

    [HttpGet]
    public ActionResult<IEnumerable<BusinessDto>> GetBusinesses()
    {
      _logger.LogInformation("Get Route has been hit");
      return Ok(_dBContext.Businesses.ToList());
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
        _logger.LogError("Id was not provided to get business by id");
        return BadRequest(new { message =  "Invalid Request"});
      }
      //var business = BusinessStore.businessList.FirstOrDefault(b => b.Id == Guid.Parse(id));
      var business = _dBContext.Businesses.FirstOrDefault(b => b.Id == Guid.Parse(id));

      if (business == null)
      {
       _logger.LogError($"Business with id {id} not found");
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
      if(_dBContext.Businesses.FirstOrDefault(b => b.Name.ToLower() == business.Name.ToLower()) != null)
      {
        ModelState.AddModelError("InvalidName", "Business already exist");
        return BadRequest(ModelState);
      }
      if (business == null || business.Name == null)
      {
        return BadRequest(new { message = "Invalid Request" });
      }
      Business model = new()
      { 
        
        Name = business.Name, 
        City = business.City,
        Address = business.Address,
        Country = business.Country,
        ImageUrl = business.ImageUrl,
        Phone = business.Phone,
        PostalCode = business.PostalCode,
        State = business.State,
      };

      _dBContext.Businesses.Add(model);

      _dBContext.SaveChanges();

      return CreatedAtRoute("GetBusiness", new { id= model.Id}, model);
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

      var business = _dBContext.Businesses.FirstOrDefault(b => b.Id == Guid.Parse(id));

      if(business == null)
      {
        return NotFound(new { message = "Business not found" });
      }

      _dBContext.Businesses.Remove(business);
      _dBContext.SaveChanges();

      return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBusiness(string id, [FromBody] BusinessDto business)
    {
      if (Guid.TryParse(id, out Guid _) == false)
      {
        return BadRequest(new { message = "Invalid ID" });
      }
      if (business == null || Guid.Parse(id) != business.Id)
      {
        return BadRequest(new { message = "Invalid request" });
      }
      var currentBusiness = _dBContext.Businesses.FirstOrDefault(b => b.Id == Guid.Parse(id));

      if (currentBusiness == null)
      {
        return NotFound(new { message = "Business not found" });
      }
      // Update properties of the existing entity
      currentBusiness.Name = business.Name;
      currentBusiness.Address = business.Address;
      currentBusiness.Phone = business.Phone;
      currentBusiness.City = business.City;
      currentBusiness.State = business.State;
      currentBusiness.Country = business.Country;
      currentBusiness.UpdatedDate = DateTime.UtcNow; // Use UTC time

      _dBContext.Businesses.Update(currentBusiness);
      _dBContext.SaveChanges();

      return Ok(currentBusiness);
    }

    [HttpPatch("{id}")]
    public IActionResult UpdatePartialBusiness(string id, JsonPatchDocument<Business> business)
    {
      if (Guid.TryParse(id, out Guid _) == false)
      {
        return BadRequest(new { message = "Invalid request" });
      }

      Business? currentBusiness = _dBContext.Businesses.FirstOrDefault(b => b.Id == Guid.Parse(id));

      
      if (currentBusiness == null)
      {
        return NotFound(new { message = "Business not found" });
      }


      business.ApplyTo(currentBusiness);

      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _dBContext.Businesses.Update(currentBusiness);

      _dBContext.SaveChanges();

      return Ok(currentBusiness);
    }
  }
}
