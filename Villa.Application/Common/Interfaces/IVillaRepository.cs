using Villa.Domain.Entities;

namespace Villa.Application.Common.Interfaces;

public interface IVillaRepository : IRepository<VillaModel>
{
    void Update(VillaModel villa);
}
