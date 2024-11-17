using System;
using Villa.Web.Models.Dtos.Villa;

namespace Villa.Web.Services.IServices;

public interface IVillaApiClient
{
    Task<IEnumerable<VillaDto>> GetAllVillasAsync();
    Task<VillaDto> GetVillaAsync(int id);
    Task<VillaDto> CreateVillaAsync(VillaCreateDto createDto);
    Task UpdateVillaAsync(int id, VillaUpdateDto updateDto);
    Task DeleteVillaAsync(int id);
}