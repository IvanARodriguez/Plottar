using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Plottar_API.Models.Dto
{
  public class BusinessDto
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set;}

    public string? Country { get; set; }

    [MaxLength(5, ErrorMessage = "Invalid postal code")]
    public string? Phone { get; set; }
    
    }
}
