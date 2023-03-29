namespace Puzzle.Lib.Http.Services
{
    public interface IRestService
    {
        /// <summary>
        /// Sends an asynchronous HTTP request using the specified client name and route URL, and returns the expected response type as Task<TResponse>.
        /// A cancellation token can be provided to cancel the request.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type</typeparam>
        /// <param name="clientName">The name of the client to use for the request</param>
        /// <param name="routeUrl">The URL of the route to request</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the request</param>
        /// <returns>A Task object representing the asynchronous operation that returns the expected response type</returns>
        /// <exception cref="ArgumentNullException">throws when parameters are empty or null</exception>
        Task<TResponse> GetResponseAsync<TResponse>(string clientName, string routeUrl, CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends an asynchronous HTTP request using the specified client name and route URL, and returns the expected response type as Task<TResponse>.
        /// Additional query parameters can be provided using a NameValueCollection.
        /// A cancellation token can be provided to cancel the request.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type</typeparam>
        /// <param name="clientName">The name of the client to use for the request</param>
        /// <param name="routeUrl">The URL of the route to request</param>
        /// <param name="nameValueCollection">A collection of query parameters to include in the request URL</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the request</param>
        /// <returns>A Task object representing the asynchronous operation that returns the expected response type</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> GetResponseAsync<TResponse>(
            string clientName, 
            string routeUrl, 
            NameValueCollection nameValueCollection, 
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends an asynchronous HTTP POST request using the specified client name and route URL, and returns the expected response type as Task<TResponse>.
        /// A cancellation token can be provided to cancel the request.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type</typeparam>
        /// <param name="clientName">The name of the client to use for the request</param>
        /// <param name="routeUrl">The URL of the route to request</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the request</param>
        /// <returns>A Task object representing the asynchronous operation that returns the expected response type</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostResponseAsync<TResponse>(string clientName, string routeUrl, CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends an asynchronous HTTP POST request using the specified client name and route URL, with the specified parameters,
        /// and returns the expected response type as Task<TResponse>. A cancellation token can be provided to cancel the request.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type</typeparam>
        /// <param name="clientName">The name of the client to use for the request</param>
        /// <param name="routeUrl">The URL of the route to request</param>
        /// <param name="parameters">The object representing the parameters to include in the request</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the request</param>
        /// <returns>A Task object representing the asynchronous operation that returns the expected response type</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostResponseAsync<TResponse>(
            string clientName, 
            string routeUrl, 
            object parameters, 
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends an asynchronous HTTP POST request using the specified client name and route URL, with the specified parameters and query parameters,
        /// and returns the expected response type as Task<TResponse>. A cancellation token can be provided to cancel the request.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type</typeparam>
        /// <param name="clientName">The name of the client to use for the request</param>
        /// <param name="routeUrl">The URL of the route to request</param>
        /// <param name="parameters">The object representing the parameters to include in the request body</param>
        /// <param name="nameValueCollection">A collection of query parameters to include in the request URL</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the request</param>
        /// <returns>A Task object representing the asynchronous operation that returns the expected response type</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostResponseAsync<TResponse>(
            string clientName,
            string routeUrl,
            object parameters,
            NameValueCollection nameValueCollection,
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends an asynchronous HTTP POST request with multipart/form-data content using the specified client name and route URL,
        /// and returns the expected response type as Task<TResponse>. A cancellation token can be provided to cancel the request.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type</typeparam>
        /// <param name="clientName">The name of the client to use for the request</param>
        /// <param name="routeUrl">The URL of the route to request</param>
        /// <param name="multipartFormDataContent">The multipart/form-data content to include in the request</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the request</param>
        /// <returns>A Task object representing the asynchronous operation that returns the expected response type</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostMultipartDataResponseAsync<TResponse>(
            string clientName,
            string routeUrl,
            MultipartFormDataContent multipartFormDataContent,
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends an asynchronous HTTP POST request with multipart/form-data content using the specified client name and route URL,
        /// with the specified query parameters, and returns the expected response type as Task<TResponse>.
        /// A cancellation token can be provided to cancel the request.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type</typeparam>
        /// <param name="clientName">The name of the client to use for the request</param>
        /// <param name="routeUrl">The URL of the route to request</param>
        /// <param name="multipartFormDataContent">The multipart/form-data content to include in the request</param>
        /// <param name="nameValueCollection">A collection of query parameters to include in the request URL</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the request</param>
        /// <returns>A Task object representing the asynchronous operation that returns the expected response type</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostMultipartDataResponseAsync<TResponse>(
            string clientName,
            string routeUrl,
            MultipartFormDataContent multipartFormDataContent,
            NameValueCollection nameValueCollection,
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends an asynchronous HTTP POST request with URL encoded content using the specified client name and route URL,
        /// with the specified key-value pairs, and returns the expected response type as Task<TResponse>.
        /// A cancellation token can be provided to cancel the request.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type</typeparam>
        /// <param name="clientName">The name of the client to use for the request</param>
        /// <param name="routeUrl">The URL of the route to request</param>
        /// <param name="keyValuePairs">The key-value pairs to include in the request body as URL-encoded content</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the request</param>
        /// <returns>A Task object representing the asynchronous operation that returns the expected response type</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostEncodedResponseAsync<TResponse>(
            string clientName, 
            string routeUrl, 
            IList<KeyValuePair<string, string>> keyValuePairs, 
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends an asynchronous HTTP POST request with URL encoded content using the specified client name and route URL,
        /// with the specified key-value pairs and query parameters, and returns the expected response type as Task<TResponse>.
        /// A cancellation token can be provided to cancel the request.
        /// </summary>
        /// <typeparam name="TResponse">The expected response type</typeparam>
        /// <param name="clientName">The name of the client to use for the request</param>
        /// <param name="routeUrl">The URL of the route to request</param>
        /// <param name="keyValuePairs">The key-value pairs to include in the request body as URL-encoded content</param>
        /// <param name="nameValueCollection">A collection of query parameters to include in the request URL</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the request</param>
        /// <returns>A Task object representing the asynchronous operation that returns the expected response type</returns>
        /// <exception cref="ArgumentNullException">thrown when http client type cannot be created</exception>
        Task<TResponse> PostEncodedResponseAsync<TResponse>(
            string clientName,
            string routeUrl,
            IList<KeyValuePair<string, string>> keyValuePairs,
            NameValueCollection nameValueCollection,
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends a PUT request to the specified API endpoint with the specified parameters and returns the response as the specified type. 
        /// </summary>
        /// <typeparam name="TResponse">The type of response expected from the API endpoint. This must be a class type that has a parameterless constructor.</typeparam>
        /// <param name="clientName">The name of the HTTP client to use for the request.</param>
        /// <param name="routeUrl">The URL of the API endpoint to send the request to.</param>
        /// <param name="parameters">The parameters to include in the request body.</param>
        /// <param name="cancellationToken">The cancellation token that can be used to cancel the request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized response object.</returns>

        Task<TResponse> PutApiResponseAsync<TResponse>(
            string clientName, 
            string routeUrl, 
            object parameters, 
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends a PUT request to the specified API endpoint with the specified parameters and headers, and returns the response as the specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type of response expected from the API endpoint. This must be a class type that has a parameterless constructor.</typeparam>
        /// <param name="clientName">The name of the HTTP client to use for the request.</param>
        /// <param name="routeUrl">The URL of the API endpoint to send the request to.</param>
        /// <param name="parameters">The parameters to include in the request body.</param>
        /// <param name="nameValueCollection">The collection of headers to include in the request.</param>
        /// <param name="cancellationToken">The cancellation token that can be used to cancel the request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized response object.</returns>

        Task<TResponse> PutApiResponseAsync<TResponse>(
            string clientName, 
            string routeUrl, 
            object parameters, 
            NameValueCollection nameValueCollection, 
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends a DELETE request to the specified API endpoint and returns the response as the specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type of response expected from the API endpoint. This must be a class type that has a parameterless constructor.</typeparam>
        /// <param name="clientName">The name of the HTTP client to use for the request.</param>
        /// <param name="routeUrl">The URL of the API endpoint to send the request to.</param>
        /// <param name="cancellationToken">The cancellation token that can be used to cancel the request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized response object.</returns>

        Task<TResponse> DeleteApiResponseAsync<TResponse>(
            string clientName, 
            string routeUrl, 
            CancellationToken cancellationToken = default) where TResponse : class, new();

        /// <summary>
        /// Sends a DELETE request to the specified API endpoint with the specified headers and returns the response as the specified type.
        /// </summary>
        /// <typeparam name="TResponse">The type of response expected from the API endpoint. This must be a class type that has a parameterless constructor.</typeparam>
        /// <param name="clientName">The name of the HTTP client to use for the request.</param>
        /// <param name="routeUrl">The URL of the API endpoint to send the request to.</param>
        /// <param name="headersCollection">The collection of headers to include in the request.</param>
        /// <param name="cancellationToken">The cancellation token that can be used to cancel the request.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the deserialized response object.</returns>
        Task<TResponse> DeleteApiResponseAsync<TResponse>(
            string clientName, 
            string routeUrl, 
            NameValueCollection nameValueCollection, 
            CancellationToken cancellationToken = default) where TResponse : class, new();
    }
}
