﻿using System.ComponentModel.DataAnnotations;

namespace Villa.Web.Models.Dtos.Villa;

public class VillaUpdateDto
{
    public int Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    public int Sqft { get; set; }
    [Required]
    public int Occupancy { get; set; }
    [Required]
    public double Rate { get; set; }
    [Required]
    public string ImageUrl { get; set; }
    [Required]
    public string Amenity { get; set; }
    public string Details { get; set; }
}
