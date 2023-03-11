namespace Puzzle.Lib.Http.Services
{
    public interface IHttpService
    {
        /// <summary>
        /// Http get request
        /// </summary>
        /// <typeparam name="TResponse">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> GetResponseAsync<TResponse>(string clientName, string routeUrl) where TResponse : class, new();

        /// <summary>
        /// Http get request
        /// </summary>
        /// <typeparam name="TResponse">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="nameValueCollection">header information to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> GetResponseAsync<TResponse>(string clientName, string routeUrl, NameValueCollection nameValueCollection) where TResponse : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TResponse">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostResponseAsync<TResponse>(string clientName, string routeUrl) where TResponse : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TResponse">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="parameters">optional body information. (class to be posted)</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostResponseAsync<TResponse>(string clientName, string routeUrl, object parameters) where TResponse : class, new();

        /// <summary>
        /// Http post request
        /// </summary>
        /// <typeparam name="TResponse">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="parameters">optional body information. (class to be posted)</param>
        /// <param name="nameValueCollection">header information to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostResponseAsync<TResponse>(string clientName, string routeUrl, object parameters, NameValueCollection nameValueCollection) where TResponse : class, new();

        /// <summary>
        /// Http post request - multipart form data content
        /// </summary>
        /// <typeparam name="TResponse">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="multipartFormDataContent">multipart form data content to be submitted</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostMultipartDataResponseAsync<TResponse>(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent) where TResponse : class, new();

        /// <summary>
        /// Http post request - multipart form data content
        /// </summary>
        /// <typeparam name="TResponse">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="multipartFormDataContent">multipart form data content to be submitted</param>
        /// <param name="nameValueCollection">header information to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostMultipartDataResponseAsync<TResponse>(string clientName, string routeUrl, MultipartFormDataContent multipartFormDataContent, NameValueCollection nameValueCollection) where TResponse : class, new();

        /// <summary>
        /// Http post request - form url encoded content
        /// </summary>
        /// <typeparam name="TResponse">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="keyValuePairs">encoded list to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostEncodedResponseAsync<TResponse>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs) where TResponse : class, new();

        /// <summary>
        /// Http post request - form url encoded content
        /// </summary>
        /// <typeparam name="TResponse">object instantiable concrete class</typeparam>
        /// <param name="clientName">http client name registered in container</param>
        /// <param name="routeUrl">the route address to which the request will be made</param>
        /// <param name="keyValuePairs">encoded list to be sent with the request</param>
        /// <param name="nameValueCollection">header information to be sent with the request</param>
        /// <returns>specified, instantiable concrete class</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostEncodedResponseAsync<TResponse>(string clientName, string routeUrl, IList<KeyValuePair<string, string>> keyValuePairs, NameValueCollection nameValueCollection) where TResponse : class, new();
    }
}
