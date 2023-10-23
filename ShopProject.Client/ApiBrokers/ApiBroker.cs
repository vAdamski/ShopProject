using System.Net.Http.Json;

namespace ShopProject.Client.ApiBrokers;

public class ApiBroker : IApiBroker
{
    private readonly HttpClient _httpClient;

    public ApiBroker(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    protected async Task<T> GetAsync<T>(string relativeUrl) =>
        await _httpClient.GetFromJsonAsync<T>(relativeUrl);
    
    protected async Task PostAsync<T>(string relativeUrl, T content) =>
        await _httpClient.PostAsJsonAsync<T>(relativeUrl, content);

    protected async Task DeleteAsync(string relativeUrl, int id) =>
        await _httpClient.DeleteAsync($"{relativeUrl}/{id}");

    protected async Task PutAsync<T>(string relativeUrl, T content) =>
        await _httpClient.PutAsJsonAsync(relativeUrl, content);
}