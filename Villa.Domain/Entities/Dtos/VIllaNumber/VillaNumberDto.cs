using System.ComponentModel.DataAnnotations;

namespace Villa.Domain.Entities.Dtos.VIllaNumber;

public class VillaNumberDto
{
    public int VillaNo { get; set; }
    public string SpecialDetails { get; set; } = string.Empty;

}
