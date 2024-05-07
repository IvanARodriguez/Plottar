using AutoMapper;
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
  public class BusinessController(ILogger<BusinessController> logger, ApplicationDBContext dBContext, IMapper mapper) : ControllerBase
  {
    private readonly ILogger<BusinessController> _logger = logger;
    private readonly ApplicationDBContext _dBContext = dBContext;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BusinessDto>>> GetBusinessesAsync()
    {
      _logger.LogInformation("Get Route has been hit");
      IEnumerable<Business> businessList = await _dBContext.Businesses.ToListAsync();
      return Ok(_mapper.Map<IEnumerable<BusinessDto>>(businessList));
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

      return Ok(_mapper.Map<BusinessDto>(business));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BusinessDto>> CreateBusinessAsync([FromBody] BusinessCreateDto businessCreateDto)
    {
      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      if (await _dBContext.Businesses.FirstOrDefaultAsync(b => b.Name.ToLower() == businessCreateDto.Name.ToLower()) != null)
      {
        ModelState.AddModelError("InvalidName", "Business already exist");
        return BadRequest(ModelState);
      }
      if (businessCreateDto == null || businessCreateDto.Name == null)
      {
        return BadRequest(new { message = "Invalid Request" });
      }
      Business model = _mapper.Map<Business>(businessCreateDto);
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
    public async Task <IActionResult> UpdateBusiness(string id, [FromBody] BusinessUpdateDto businessUpdateDto)
    {
      if (Guid.TryParse(id, out Guid _) == false)
      {
        return BadRequest(new { message = "Invalid ID" });
      }

      Business business = _mapper.Map<Business>(businessUpdateDto);

      var currentBusiness = await _dBContext.Businesses.FirstOrDefaultAsync(b => b.Id == Guid.Parse(id));

      if (currentBusiness == null)
      {
        return NotFound(new { message = "Business not found" });
      }
      

      _dBContext.Businesses.Update(currentBusiness);
      await _dBContext.SaveChangesAsync();

      return Ok(currentBusiness);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePartialBusiness(string id, JsonPatchDocument<BusinessUpdateDto> patchDto)
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
      BusinessUpdateDto businessDto = _mapper.Map<BusinessUpdateDto>(business);

      patchDto.ApplyTo(businessDto, ModelState);

      if(!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      
      Business model = _mapper.Map<Business>(businessDto);

      _dBContext.Businesses.Update(business);

      await _dBContext.SaveChangesAsync();

      return Ok(businessDto);
    }
  }
}
