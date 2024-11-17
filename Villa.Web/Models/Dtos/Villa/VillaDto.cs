using System.ComponentModel.DataAnnotations;

namespace Villa.Web.Models.Dtos.Villa;

public class VillaDto
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
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
    public string Details { get; set; }
}
