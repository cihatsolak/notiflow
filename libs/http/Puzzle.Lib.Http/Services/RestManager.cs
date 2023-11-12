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

    public async Task<TResponse> GetResponseAsync<TResponse>(string clientName, string routeUrl, CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(routeUrl, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> GetResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(routeUrl, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostResponseAsync<TResponse>(string clientName, string routeUrl, CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, default(TResponse), cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, parameters);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, parameters, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostMultipartDataResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        MultipartFormDataContent multipartFormDataContent,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, multipartFormDataContent);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(routeUrl, multipartFormDataContent, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostMultipartDataResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        MultipartFormDataContent multipartFormDataContent,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, multipartFormDataContent, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(routeUrl, multipartFormDataContent, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostEncodedResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        IList<KeyValuePair<string, string>> keyValuePairs,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, keyValuePairs);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        using FormUrlEncodedContent content = new(keyValuePairs);
        HttpRequestMessage request = new(HttpMethod.Post, routeUrl) { Content = content };

        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PostEncodedResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        IList<KeyValuePair<string, string>> keyValuePairs,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, keyValuePairs, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        using FormUrlEncodedContent content = new(keyValuePairs);
        HttpRequestMessage request = new(HttpMethod.Post, routeUrl) { Content = content };

        HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PatchResponseAsync<TResponse>(string clientName, string routeUrl, CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PatchAsJsonAsync(routeUrl, default(TResponse), cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PatchResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, parameters);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PatchAsJsonAsync(routeUrl, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PatchResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, parameters, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PatchAsJsonAsync(routeUrl, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PutApiResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, parameters);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(routeUrl, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> PutApiResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        object parameters,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, parameters, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.PutAsJsonAsync(routeUrl, parameters, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> DeleteApiResponseAsync<TResponse>(string clientName, string routeUrl, CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(routeUrl, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    public async Task<TResponse> DeleteApiResponseAsync<TResponse>(
        string clientName,
        string routeUrl,
        NameValueCollection nameValueCollection,
        CancellationToken cancellationToken) where TResponse : class, new()
    {
        CheckArguments(clientName, routeUrl, nameValueCollection);

        HttpClient httpClient = _httpClientFactory.CreateClient(clientName);

        foreach (var key in nameValueCollection.AllKeys)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
        }

        HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(routeUrl, cancellationToken).ConfigureAwait(false);
        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogWarning("Request sent to {routeUrl} but no positive response. {@httpResponseMessage}", routeUrl, httpResponseMessage);
        }

        return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    private static void CheckArguments(string clientName, string routeUrl)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
    }

    private static void CheckArguments(string clientName, string routeUrl, NameValueCollection nameValueCollection)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(nameValueCollection);
    }

    private static void CheckArguments(string clientName, string routeUrl, object parameters)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(parameters);
    }

    private static void CheckArguments(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(keyValuePairs);
    }

    private static void CheckArguments(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs, NameValueCollection nameValueCollection)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        ArgumentNullException.ThrowIfNull(nameValueCollection);
    }

    private static void CheckArguments(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(multipartFormDataContent);
    }

    private static void CheckArguments(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent, NameValueCollection nameValueCollection)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(multipartFormDataContent);
        ArgumentNullException.ThrowIfNull(nameValueCollection);
    }

    private static void CheckArguments(string clientName, string routeUrl, object parameters, NameValueCollection nameValueCollection)
    {
        ArgumentException.ThrowIfNullOrEmpty(clientName);
        ArgumentException.ThrowIfNullOrEmpty(routeUrl);
        ArgumentNullException.ThrowIfNull(parameters);
        ArgumentNullException.ThrowIfNull(nameValueCollection);
    }
}
