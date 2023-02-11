namespace Notiflow.Lib.Http.Services
{
    internal sealed class RestManager : IRestService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RestManager(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task<TModel> GetResponseAsync<TModel>(string clientName, string routeUrl) where TModel : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> GetResponseAsync<TModel>(string clientName, string routeUrl, NameValueCollection headersCollection) where TModel : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> PostEncodedResponseAsync<TModel>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs) where TModel : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> PostEncodedResponseAsync<TModel>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs, NameValueCollection headersCollection) where TModel : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> PostMultipartDataResponseAsync<TModel>(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent) where TModel : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> PostMultipartDataResponseAsync<TModel>(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent, NameValueCollection headersCollection) where TModel : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> PostResponseAsync<TModel>(string clientName, string routeUrl) where TModel : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> PostResponseAsync<TModel>(string clientName, string routeUrl, object parameters) where TModel : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<TModel> PostResponseAsync<TModel>(string clientName, string routeUrl, object parameters, NameValueCollection headersCollection) where TModel : class, new()
        {
            throw new NotImplementedException();
        }
    }
}
