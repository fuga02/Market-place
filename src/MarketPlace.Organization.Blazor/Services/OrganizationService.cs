using Blazored.LocalStorage;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MarketPlace.Organization.Blazor.Services;

public class OrganizationService
{
    private readonly ILocalStorageService _storage;
    private readonly HttpClient _httpClient;

    public OrganizationService(ILocalStorageService storage, HttpClient httpClient)
    {
        _storage = storage;
        _httpClient = httpClient;
    }

    public async Task<T?> Get<T>(string url) where T : class
    {
        //var token = await _storage.GetItemAsStringAsync("token");
        //var request = new HttpRequestMessage(HttpMethod.Get, url);
        //request.Headers.Add("Authorization", $"Bearer {token}");
        //var response = await _httpClient.SendAsync(request);
        //return (await response.Content.ReadFromJsonAsync<T>())!;

        return await Send<T>(url, HttpMethod.Post, null);
    }
    public async Task Post<T>(string url,T t) where T : class
    {
    
        //    var token = await _storage.GetItemAsStringAsync("token");
    //    var request = new HttpRequestMessage(HttpMethod.Post, url);
    //    request.Headers.Add("Authorization", $"Bearer {token}");
    //    request.Content = JsonContent.Create(t);
    //    await _httpClient.SendAsync(request);
    //    var response = await _httpClient.SendAsync(request);

        await Send<object>(url, HttpMethod.Post, JsonContent.Create(t));
    }

    public async Task<T?> Send<T>(string url, HttpMethod method, HttpContent? content) where T : class
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(method, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        
        if(content != null) { }
            request.Content = content;

        await _httpClient.SendAsync(request);
        var response = await _httpClient.SendAsync(request);
        var data = await response.Content.ReadAsStringAsync();
        return  JsonSerializer.Deserialize<T>(data);
    }
}