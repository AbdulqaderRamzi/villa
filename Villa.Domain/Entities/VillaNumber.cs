using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Villa.Domain.Entities;

public class VillaNumber
{
    [Key]
    public int VillaNo { get; set; }
    public string SpecialDetails { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;

    public int VillaId { get; set; }
    [ForeignKey("VillaId")]
    public VillaModel? Villa { get; set; }
}
