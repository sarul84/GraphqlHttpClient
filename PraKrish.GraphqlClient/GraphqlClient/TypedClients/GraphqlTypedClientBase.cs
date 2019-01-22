//-----------------------------------------------------------------------
// <copyright file="GraphqlTypedClientBase.cs" company="PraKrish Technologies">
//     Copyright (c) 2019 PraKrish Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>01/17/2019</date>
// <summary>The http typed client base</summary>
//-----------------------------------------------------------------------

namespace GraphqlClient.TypedClients
{
    using GraphqlClient.Reponse;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Base class for typed clients
    /// </summary>
    public abstract class GraphqlTypedClientBase
    {
        /// <summary>
        /// Constant for media type header
        /// </summary>
        private const string MediaType = "application/json";

        /// <summary>
        /// Holds httpclient object instance
        /// </summary>
        protected readonly HttpClient Client;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphqlTypedClientBase.cs"/> class.
        /// </summary>
        /// <param name="client">The http client object</param>
        public GraphqlTypedClientBase(HttpClient client)
        {
            this.Client = client;
            this.Client.DefaultRequestHeaders.Connection.Add("keep-alive");
        }

        /// <summary>
        /// Adds the specified header and its value into the System.Net.Http.Headers.HttpHeaders
        /// collection.
        /// </summary>
        /// <param name="name">The header to add to the collection.</param>
        /// <param name="value">The content of the header.</param>
        public void AddDefaultRequestHeader(string name, string value)
        {
            this.AddMediaType(MediaType);
            this.Client.DefaultRequestHeaders.Remove(name);
            this.Client.DefaultRequestHeaders.Add(name, value);
        }

        /// <summary>
        /// Adds the specified header and its values into the System.Net.Http.Headers.HttpHeaders
        /// collection.
        /// </summary>
        /// <param name="name">The header to add to the collection.</param>
        /// <param name="values">A list of header values to add to the collection.</param>
        public void AddDefaultRequestHeader(string name, IEnumerable<string> values)
        {
            this.AddMediaType(MediaType);
            this.Client.DefaultRequestHeaders.Remove(name);
            this.Client.DefaultRequestHeaders.Add(name, values);
        }

        /// <summary>
        /// Adds authentication token to default request header
        /// </summary>
        /// <param name="authorizationToken">The authorization token</param>
        /// <param name="authorizationMethod">The authorization method</param>
        public void AddAuthorization(string authorizationToken, string authorizationMethod = "Bearer")
        {
            this.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
        }

        /// <summary>
        /// Creates http request message object
        /// </summary>
        /// <param name="httpMethod">Http operation method type</param>
        /// <param name="query">The graphql query</param>
        /// <param name="url">The graphql api Uri</param>
        /// <returns>The new http request message object</returns>
        public HttpRequestMessage AddHttpRequestMessage(HttpMethod httpMethod, string query, string url)
        {
            return new HttpRequestMessage(httpMethod, url)
            {
                Content = new StringContent(query, Encoding.UTF8, MediaType)
            };
        }

        /// <summary>
        /// Send a http request message to http client with specified Uri
        /// </summary>
        /// <typeparam name="TEntity">The type to which response would be deserialized</typeparam>
        /// <param name="httpRequestMessage">The http request message object</param>
        /// <returns>The graphql response object</returns>
        public async Task<IGraphqlResponse<TEntity>> SendAsync<TEntity>(HttpRequestMessage httpRequestMessage,
            CancellationToken cancellationToken)
            where TEntity : class
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = await this.Client.SendAsync(httpRequestMessage, cancellationToken).ConfigureAwait(false);
            if (response?.IsSuccessStatusCode == false)
            {
                var errorResponse = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);
                var result = new GraphqlResponse<TEntity>()
                {
                    Data = default(TEntity),
                    StatusCode = response.StatusCode,
                    IsSuccessCode = response.IsSuccessStatusCode,
                    Errors = new Collection<GraphqlError>
                    {
                        new GraphqlError
                        {
                            Message = errorResponse
                        }
                    }
                };
                stopwatch.Stop();
                result.ElapsedTime = stopwatch.ElapsedMilliseconds;
                return result;
            }

            var jsonString = await (response?.Content?.ReadAsStringAsync()).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(jsonString))
            {
                var result = JsonConvert.DeserializeObject<GraphqlResponse<TEntity>>(jsonString);
                stopwatch.Stop();
                result.StatusCode = response.StatusCode;
                result.IsSuccessCode = response.IsSuccessStatusCode;
                result.ElapsedTime = stopwatch.ElapsedMilliseconds;
                return result;
            }

            return null;
        }

        /// <summary>
        /// Add the value of the Accept header for an HTTP request.
        /// </summary>
        /// <param name="mediaType">The accept header media type</param>
        private void AddMediaType(string mediaType)
        {
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }
    }
}
