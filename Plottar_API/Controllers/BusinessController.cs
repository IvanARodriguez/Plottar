using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<IEnumerable<BusinessDto>>> GetBusinessesAsync()
    {
      _logger.LogInformation("Get Route has been hit");
      return Ok(await _dBContext.Businesses.ToListAsync());
    }

    [HttpGet("id", Name ="GetBusiness")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BusinessDto>> GetBusinessAsync(string id)
    {
      bool isValidGuid = Guid.TryParse(id, out _);

      if (String.IsNullOrEmpty(id) || !isValidGuid)
      {
        _logger.LogError("Id was not provided to get business by id");
        return BadRequest(new { message =  "Invalid Request"});
      }
      //var business = BusinessStore.businessList.FirstOrDefault(b => b.Id == Guid.Parse(id));
      var business = await _dBContext.Businesses.FirstOrDefaultAsync(b => b.Id == Guid.Parse(id));

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
    public async Task<ActionResult<BusinessDto>> CreateBusinessAsync([FromBody] BusinessDto business)
    {
      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if (await _dBContext.Businesses.FirstOrDefaultAsync(b => b.Name.ToLower() == business.Name.ToLower()) != null)
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

      await _dBContext.Businesses.AddAsync(model);

      await _dBContext.SaveChangesAsync();

      return CreatedAtRoute("GetBusiness", new { id= model.Id}, model);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBusinessAsync(string id)
    {
      if(Guid.TryParse(id, out Guid _) == false)
      {
        return BadRequest(new { message = "Invalid request" });
      }

      var business = await _dBContext.Businesses.FirstOrDefaultAsync(b => b.Id == Guid.Parse(id));

      if(business == null)
      {
        return NotFound(new { message = "Business not found" });
      }

      _dBContext.Businesses.Remove(business);
      await _dBContext.SaveChangesAsync();

      return NoContent();
    }

    [HttpPut("{id}")]
    public async Task <IActionResult> UpdateBusiness(string id, [FromBody] BusinessDto businessDto)
    {
      if (Guid.TryParse(id, out Guid _) == false)
      {
        return BadRequest(new { message = "Invalid ID" });
      }
      if (businessDto == null || Guid.Parse(id) != businessDto.Id)
      {
        return BadRequest(new { message = "Invalid request" });
      }
      var currentBusiness = await _dBContext.Businesses.FirstOrDefaultAsync(b => b.Id == Guid.Parse(id));

      if (currentBusiness == null)
      {
        return NotFound(new { message = "Business not found" });
      }
      // Update properties of the existing entity
      currentBusiness.Name = businessDto.Name;
      currentBusiness.Address = businessDto.Address;
      currentBusiness.Phone = businessDto.Phone;
      currentBusiness.City = businessDto.City;
      currentBusiness.State = businessDto.State;
      currentBusiness.Country = businessDto.Country;
      currentBusiness.UpdatedDate = DateTime.UtcNow; // Use UTC time

      _dBContext.Businesses.Update(currentBusiness);
      await _dBContext.SaveChangesAsync();

      return Ok(currentBusiness);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePartialBusiness(string id, JsonPatchDocument<BusinessDto> patchDto)
    {
      if (Guid.TryParse(id, out Guid _) == false)
      {
        return BadRequest(new { message = "Invalid request" });
      }

      var business = await _dBContext.Businesses.AsNoTracking().FirstOrDefaultAsync(b => b.Id == Guid.Parse(id));

      if (business == null)
      {
        return NotFound(new { message = "Business not found" });
      }
      BusinessDto businessDto = new() 
      {
        Id = business.Id, 
        Name = business.Name,
        Address = business.Address,
        Phone = business.Phone,
        City = business.City,
        State = business.State,
        Country = business.Country,
        UpdatedDate = DateTime.UtcNow

      };
      
      patchDto.ApplyTo(businessDto, ModelState);

      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      business.Name = businessDto.Name;
      business.Address = businessDto.Address;
      business.Phone = businessDto.Phone;
      business.City = businessDto.City;
      business.State = businessDto.State;
      business.Country = businessDto.Country;
      business.UpdatedDate = businessDto.UpdatedDate;

      _dBContext.Businesses.Update(business);

      await _dBContext.SaveChangesAsync();

      return Ok(businessDto);
    }
  }
}
