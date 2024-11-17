namespace Villa.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<bool> SaveAsync();
    public IVillaRepository Villa { get; set; }
    public IVillaNumberRepository VillaNumber { get; set; }
}