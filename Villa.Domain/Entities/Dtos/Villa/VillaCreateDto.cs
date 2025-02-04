﻿using System.ComponentModel.DataAnnotations;

namespace Villa.Domain.Entities.Dtos.Villa;
public class VillaCreateDto
{
    [MaxLength(30)]
    public string Name { get; set; }

    public int Sqft { get; set; }

    public int Occupancy { get; set; }

    public double Rate { get; set; }

    public string? ImageUrl { get; set; }
    public string? Amenity { get; set; }
    public string? Details { get; set; }
}
