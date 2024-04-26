namespace Plottar_API.Models.Dto
{
  public class BusinessDto
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
   }
}
