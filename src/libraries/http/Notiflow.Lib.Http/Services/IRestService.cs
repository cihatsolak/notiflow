namespace Notiflow.Lib.Http.Services
{
    public interface IRestService
    {
        /// <summary>
        /// Http get request
        /// </summary>
        /// <typeparam name="TModel">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TModel> GetResponseAsync<TModel>(string clientName, string routeUrl) where TModel : class, new();

        /// <summary>
        /// Http get request
        /// </summary>
        /// <typeparam name="TModel">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="headersCollection">header information to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TModel> GetResponseAsync<TModel>(string clientName, string routeUrl, NameValueCollection headersCollection) where TModel : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TModel">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TModel> PostResponseAsync<TModel>(string clientName, string routeUrl) where TModel : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TModel">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="parameters">optional body information. (class to be posted)</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TModel> PostResponseAsync<TModel>(string clientName, string routeUrl, object parameters) where TModel : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TModel">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="parameters">optional body information. (class to be posted)</param>
        /// <param name="headersCollection">header information to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TModel> PostResponseAsync<TModel>(string clientName, string routeUrl, object parameters, NameValueCollection headersCollection) where TModel : class, new();

        /// <summary>
        /// Http post request - multipart form data content
        /// </summary>
        /// <typeparam name="TModel">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="multipartFormDataContent">multipart form data content to be submitted</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TModel> PostMultipartDataResponseAsync<TModel>(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent) where TModel : class, new();

        /// <summary>
        /// Http post request - multipart form data content
        /// </summary>
        /// <typeparam name="TModel">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="multipartFormDataContent">multipart form data content to be submitted</param>
        /// <param name="headersCollection">header information to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TModel> PostMultipartDataResponseAsync<TModel>(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent, NameValueCollection headersCollection) where TModel : class, new();

        /// <summary>
        /// Http post request - form url encoded content
        /// </summary>
        /// <typeparam name="TModel">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="keyValuePairs">encoded list to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TModel> PostEncodedResponseAsync<TModel>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs) where TModel : class, new();

        /// <summary>
        /// Http post request - form url encoded content
        /// </summary>
        /// <typeparam name="TModel">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="keyValuePairs">encoded list to be sent with the request</param>
        /// <param name="headersCollection">header information to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TModel> PostEncodedResponseAsync<TModel>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs, NameValueCollection headersCollection) where TModel : class, new();
    }
}
