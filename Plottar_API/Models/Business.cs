﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Plottar_API.Models
{
  [Index(nameof(ImageUrl), IsUnique = true)]
  [Index(nameof(Name), IsUnique = true)]
  public class Business
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    [MaxLength(5, ErrorMessage = "Invalid postal code")]
    public string? Phone { get; set; }
  }
}
