﻿using System.Net.Http.Json;
using Blazored.LocalStorage;

namespace ChatBlazor.Services;

public class ConversationService
{
    private readonly ILocalStorageService _storage;
    private readonly HttpClient _httpClient;

    public ConversationService(ILocalStorageService storage, HttpClient httpClient)
    {
        _storage = storage;
        _httpClient = httpClient;
    }

    public async Task<T> Get<T>(string url) where T : class
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await _httpClient.SendAsync(request);
        return await response.Content.ReadFromJsonAsync<T>();
    }
}