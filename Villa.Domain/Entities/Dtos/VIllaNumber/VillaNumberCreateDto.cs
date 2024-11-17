using System.ComponentModel.DataAnnotations;

namespace Villa.Domain.Entities.Dtos.VIllaNumber;

public class VillaNumberCreateDto
{
    public int VillaNo { get; set; }
    public string SpecialDetails { get; set; } = string.Empty;
    public int VillaId { get; set; }

}
