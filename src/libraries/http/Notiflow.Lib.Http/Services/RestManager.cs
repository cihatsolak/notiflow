namespace Notiflow.Lib.Http.Services
{
    internal sealed class RestManager : IRestService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RestManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResponse> GetResponseAsync<TResponse>(string clientName, string routeUrl) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(clientName);
            ArgumentException.ThrowIfNullOrEmpty(routeUrl);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient);

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(routeUrl);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Warning("{@routeUrl} adresine istek atıldı fakat olumlu cevap alınamadı. {@httpResponseMessage}", routeUrl, httpResponseMessage);
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse> GetResponseAsync<TResponse>(string clientName, string routeUrl, NameValueCollection nameValueCollection) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(clientName);
            ArgumentException.ThrowIfNullOrEmpty(routeUrl);
            ArgumentNullException.ThrowIfNull(nameValueCollection);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient);

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(routeUrl);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Warning("{@routeUrl} adresine istek atıldı fakat olumlu cevap alınamadı. {@httpResponseMessage}", routeUrl, httpResponseMessage);
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse> PostResponseAsync<TResponse>(string clientName, string routeUrl) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(clientName);
            ArgumentException.ThrowIfNullOrEmpty(routeUrl);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient);

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, new { });
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Warning("{@routeUrl} adresine istek atıldı fakat olumlu cevap alınamadı. {@httpResponseMessage}", routeUrl, httpResponseMessage);
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse> PostResponseAsync<TResponse>(string clientName, string routeUrl, object parameters) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(clientName);
            ArgumentException.ThrowIfNullOrEmpty(routeUrl);
            ArgumentNullException.ThrowIfNull(parameters);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient);

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, parameters);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Warning("{@routeUrl} adresine istek atıldı fakat olumlu cevap alınamadı. {@httpResponseMessage}", routeUrl, httpResponseMessage);
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse> PostResponseAsync<TResponse>(string clientName, string routeUrl, object parameters, NameValueCollection nameValueCollection) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(clientName);
            ArgumentException.ThrowIfNullOrEmpty(routeUrl);
            ArgumentNullException.ThrowIfNull(parameters);
            ArgumentNullException.ThrowIfNull(nameValueCollection);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient);

            foreach (var key in nameValueCollection.AllKeys)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
            }

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(routeUrl, parameters);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Warning("{@routeUrl} adresine istek atıldı fakat olumlu cevap alınamadı. {@httpResponseMessage}", routeUrl, httpResponseMessage);
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse> PostMultipartDataResponseAsync<TResponse>(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(clientName);
            ArgumentException.ThrowIfNullOrEmpty(routeUrl);
            ArgumentNullException.ThrowIfNull(multipartFormDataContent);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient);

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(routeUrl, multipartFormDataContent);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Warning("{@routeUrl} adresine istek atıldı fakat olumlu cevap alınamadı. {@httpResponseMessage}", routeUrl, httpResponseMessage);
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse> PostMultipartDataResponseAsync<TResponse>(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent, NameValueCollection nameValueCollection) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(clientName);
            ArgumentException.ThrowIfNullOrEmpty(routeUrl);
            ArgumentNullException.ThrowIfNull(multipartFormDataContent);
            ArgumentNullException.ThrowIfNull(nameValueCollection);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient);

            foreach (var key in nameValueCollection.AllKeys)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
            }

            HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(routeUrl, multipartFormDataContent);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Warning("{@routeUrl} adresine istek atıldı fakat olumlu cevap alınamadı. {@httpResponseMessage}", routeUrl, httpResponseMessage);
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse> PostEncodedResponseAsync<TResponse>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(clientName);
            ArgumentException.ThrowIfNullOrEmpty(routeUrl);
            ArgumentNullException.ThrowIfNull(keyValuePairs);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient);

            using FormUrlEncodedContent content = new(keyValuePairs);
            HttpRequestMessage request = new(HttpMethod.Post, routeUrl) { Content = content };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Warning("{@routeUrl} adresine istek atıldı fakat olumlu cevap alınamadı. {@httpResponseMessage}", routeUrl, httpResponseMessage);
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }

        public async Task<TResponse> PostEncodedResponseAsync<TResponse>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs, NameValueCollection nameValueCollection) where TResponse : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(clientName);
            ArgumentException.ThrowIfNullOrEmpty(routeUrl);
            ArgumentNullException.ThrowIfNull(keyValuePairs);
            ArgumentNullException.ThrowIfNull(nameValueCollection);

            HttpClient httpClient = _httpClientFactory.CreateClient(clientName);
            ArgumentNullException.ThrowIfNull(httpClient);

            foreach (var key in nameValueCollection.AllKeys)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation(key, nameValueCollection[key]);
            }

            using FormUrlEncodedContent content = new(keyValuePairs);
            HttpRequestMessage request = new(HttpMethod.Post, routeUrl) { Content = content };

            HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(request);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Log.Warning("{@routeUrl} adresine istek atıldı fakat olumlu cevap alınamadı. {@httpResponseMessage}", routeUrl, httpResponseMessage);
            }

            return await httpResponseMessage.Content.ReadFromJsonAsync<TResponse>();
        }
    }
}
