using Newtonsoft.Json;
using System.Text;
using Villa.Web.Services.IServices;

namespace Villa.Web.Services;

public class BaseApiClient : IBaseApiClient
{
    protected readonly HttpClient _httpClient;

    public BaseApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(
        HttpMethod httpMethod, string endpoint, TRequest? data = default)
    {
        var request = new HttpRequestMessage(httpMethod, endpoint);

        // Post & Put
        if (data is not null)
        {
            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var result = JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());
        return result ?? throw new InvalidOperationException("Failed to deserialize the response.");
    }

}
