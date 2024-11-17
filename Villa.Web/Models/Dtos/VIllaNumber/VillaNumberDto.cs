using System.ComponentModel.DataAnnotations;

namespace Villa.Web.Models.Dtos.VIllaNumber;

public class VillaNumberDto
{
    public int VillaNo { get; set; }
    public string SpecialDetails { get; set; } = string.Empty;

}
