using Villa.Domain.Entities;

namespace Villa.Application.Common.Interfaces;

public interface IVillaNumberRepository : IRepository<VillaNumber>
{
    void Update(VillaNumber villaNumber);
}
