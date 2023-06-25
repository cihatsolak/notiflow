namespace Puzzle.Lib.Http.Services;

internal sealed class RestManager : IRestService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public RestManager(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<TResponse> GetResponseAsync<TResponse>(string clientName, string routeUrl, CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(routeUrl, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> GetResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(routeUrl, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PostResponseAsync<TResponse>(string clientName, string routeUrl, CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, default(TResponse), cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PostResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(parameters);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, parameters, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PostResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, parameters, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PostMultipartDataResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        MultipartFormDataContent multipartFormDataContent,
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(multipartFormDataContent);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(routeUrl, multipartFormDataContent, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PostMultipartDataResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        MultipartFormDataContent multipartFormDataContent,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(multipartFormDataContent);
        ArgumentNullException.ThrowIfNull(nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
       
        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(routeUrl, multipartFormDataContent, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PostEncodedResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        IList<KeyValuePair<string, string>> keyValuePairs,
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(keyValuePairs);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
        
        using FormUrlEncodedContent content = new(keyValuePairs);
        HttpRequestMessage request = new(HttpMethod.Post, routeUrl) { Content = content };

        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PostEncodedResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        IList<KeyValuePair<string, string>> keyValuePairs,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        ArgumentNullException.ThrowIfNull(nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
       
        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        using FormUrlEncodedContent content = new(keyValuePairs);
        HttpRequestMessage request = new(HttpMethod.Post, routeUrl) { Content = content };

        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PutApiResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(parameters);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(routeUrl, parameters, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> PutApiResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(routeUrl, parameters, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> DeleteApiResponseAsync<TResponse>(string clientName, string routeUrl, CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(routeUrl, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }

    public async Task<TResponse> DeleteApiResponseAsync<TResponse>(
        string clientName,
        string routeUrl, 
        NameValueCollection nameValueCollection, 
        CancellationToken cancellationToken = default) where TResponse : class, new()
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(routeUrl, cancellationToken);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            Log.Warning("Request sent to {@routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken);
    }
}
