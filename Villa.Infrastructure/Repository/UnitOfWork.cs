using Villa.Application.Common.Interfaces;
using Villa.Infrastructure.Data;

namespace Villa.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public IVillaRepository Villa { get; set; }
    public IVillaNumberRepository VillaNumber { get; set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Villa = new VillaRepository(_db);
        VillaNumber = new VillaNumberRepository(_db);
    }

    public async Task<bool> SaveAsync()
    {
        await _db.SaveChangesAsync();
        return true;
    }
}
