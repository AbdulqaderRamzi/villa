using System;
using Villa.Web.Models.Dtos.Villa;
using Villa.Web.Services.IServices;

namespace Villa.Web.Services;

public class VillaApiClient(HttpClient httpClient) : BaseApiClient(httpClient), IVillaApiClient
{
    public async Task<IEnumerable<VillaDto>> GetAllVillasAsync()
    {
        return await SendAsync<object, IEnumerable<VillaDto>>(HttpMethod.Get, "api/villas");
    }

    public async Task<VillaDto> GetVillaAsync(int id)
    {
        return await SendAsync<object, VillaDto>(HttpMethod.Get, $"api/villas/{id}");
    }

    public async Task<VillaDto> CreateVillaAsync(VillaCreateDto createDto)
    {
        return await SendAsync<VillaCreateDto, VillaDto>(HttpMethod.Post, "api/villas", createDto);
    }

    public async Task UpdateVillaAsync(int id, VillaUpdateDto updateDto)
    {
        await SendAsync<VillaUpdateDto, object>(HttpMethod.Put, $"api/villas/{id}", updateDto);
    }

    public async Task DeleteVillaAsync(int id)
    {
        await SendAsync<object, object>(HttpMethod.Delete, $"api/villas/{id}");
    }
}