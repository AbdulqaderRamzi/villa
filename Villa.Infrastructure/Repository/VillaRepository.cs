using Villa.Application.Common.Interfaces;
using Villa.Domain.Entities;
using Villa.Infrastructure.Data;

namespace Villa.Infrastructure.Repository;

public class VillaRepository : Repository<VillaModel>, IVillaRepository
{
    private readonly ApplicationDbContext _db;

    public VillaRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(VillaModel villa)
    {
        _db.Villas.Update(villa);
    }
}
