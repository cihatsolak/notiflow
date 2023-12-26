namespace Puzzle.Lib.Http.Services;

internal sealed class RestManager : IRestService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<RestManager> _logger;

    public RestManager(IHttpClientFactory httpClientFactory, ILogger<RestManager> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<TResponse> GetResponseAsync<TResponse>(string clientName, string route, CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(route, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> GetResponseAsync<TResponse>(
        string clientName,
        string route,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(route, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostResponseAsync<TResponse>(string clientName, string route, CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(route, default(TResponse), cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostResponseAsync<TResponse>(
        string clientName,
        string route,
        object parameters,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, parameters);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(route, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostResponseAsync<TResponse>(
        string clientName,
        string route,
        object parameters,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, parameters, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(route, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostMultipartDataResponseAsync<TResponse>(
        string clientName,
        string route,
        MultipartFormDataContent multipartFormDataContent,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, multipartFormDataContent);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(route, multipartFormDataContent, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostMultipartDataResponseAsync<TResponse>(
        string clientName,
        string route,
        MultipartFormDataContent multipartFormDataContent,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, multipartFormDataContent, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(route, multipartFormDataContent, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostEncodedResponseAsync<TResponse>(
        string clientName,
        string route,
        IList<KeyValuePair<string, string>> keyValuePairs,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, keyValuePairs);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        using FormUrlEncodedContent content = new(keyValuePairs);
        HttpRequestMessage request = new(HttpMethod.Post, route) { Content = content };

        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostEncodedResponseAsync<TResponse>(
        string clientName,
        string route,
        IList<KeyValuePair<string, string>> keyValuePairs,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, keyValuePairs, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        using FormUrlEncodedContent content = new(keyValuePairs);
        HttpRequestMessage request = new(HttpMethod.Post, route) { Content = content };

        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PatchResponseAsync<TResponse>(string clientName, string route, CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PatchAsJsonAsync(route, default(TResponse), cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PatchResponseAsync<TResponse>(
        string clientName,
        string route,
        object parameters,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, parameters);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PatchAsJsonAsync(route, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PatchResponseAsync<TResponse>(
        string clientName,
        string route,
        object parameters,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, parameters, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PatchAsJsonAsync(route, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PutApiResponseAsync<TResponse>(
        string clientName,
        string route,
        object parameters,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, parameters);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(route, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PutApiResponseAsync<TResponse>(
        string clientName,
        string route,
        object parameters,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, parameters, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(route, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> DeleteApiResponseAsync<TResponse>(string clientName, string route, CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(route, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> DeleteApiResponseAsync<TResponse>(
        string clientName,
        string route,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, route, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(route, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {route} but no positive response. {@httpResponseMessage}", route, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    private static void CheckArguments(string clientName, string route)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(route);
    }

    private static void CheckArguments(string clientName, string route, NameValueCollection nameValueCollection)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(route);
        ArgumentNullException.ThrowIfNull(nameValueCollection);
    }

    private static void CheckArguments(string clientName, string route, object parameters)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(route);
        ArgumentNullException.ThrowIfNull(parameters);
    }

    private static void CheckArguments(string clientName, string route, IList<KeyValuePair<string, string>> keyValuePairs)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(route);
        ArgumentNullException.ThrowIfNull(keyValuePairs);
    }

    private static void CheckArguments(string clientName, string route, IList<KeyValuePair<string, string>> keyValuePairs, NameValueCollection nameValueCollection)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(route);
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        ArgumentNullException.ThrowIfNull(nameValueCollection);
    }

    private static void CheckArguments(string clientName, string route, MultipartFormDataContent multipartFormDataContent)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(route);
        ArgumentNullException.ThrowIfNull(multipartFormDataContent);
    }

    private static void CheckArguments(string clientName, string route, MultipartFormDataContent multipartFormDataContent, NameValueCollection nameValueCollection)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(route);
        ArgumentNullException.ThrowIfNull(multipartFormDataContent);
        ArgumentNullException.ThrowIfNull(nameValueCollection);
    }

    private static void CheckArguments(string clientName, string route, object parameters, NameValueCollection nameValueCollection)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(route);
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(nameValueCollection);
    }
}
