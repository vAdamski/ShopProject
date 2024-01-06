using System.Net.Http.Json;

namespace ShopProject.Client.ApiBrokers;

public class ApiBroker : IApiBroker
{
    private readonly HttpClient _httpClient;

    public ApiBroker(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected async Task<T> GetFromJsonAsync<T>(string relativeUrl) =>
        await _httpClient.GetFromJsonAsync<T>(relativeUrl);

    protected async Task PostAsJsonAsync<T>(string relativeUrl, T content) =>
        await _httpClient.PostAsJsonAsync<T>(relativeUrl, content);

    protected async Task<HttpResponseMessage> PostAsJsonAsyncWithResponse<T>(string relativeUrl, T content)
    {
        var responseMessage = await _httpClient.PostAsJsonAsync<T>(relativeUrl, content);
        
        return responseMessage;
    }

    protected async Task PostAsync<T>(string relativeUrl, MultipartContent content) =>
        await _httpClient.PostAsync(relativeUrl, content);

    protected async Task DeleteAsync(string relativeUrl, int id) =>
        await _httpClient.DeleteAsync($"{relativeUrl}/{id}");

    protected async Task DeleteAsync(string relativeUrl) =>
        await _httpClient.DeleteAsync($"{relativeUrl}");

    protected async Task PutAsJsonAsync<T>(string relativeUrl, T content) =>
        await _httpClient.PutAsJsonAsync(relativeUrl, content);
}