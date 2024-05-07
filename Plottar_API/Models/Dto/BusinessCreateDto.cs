using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Plottar_API.Models.Dto
{
  public class BusinessCreateDto
  {

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    [MaxLength(5, ErrorMessage = "Postal code must be 5 digits")]
    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    [MaxLength(10, ErrorMessage = "Phone number should be 10 digits")]
    public string? Phone { get; set; }
  }
}
