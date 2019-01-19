//-----------------------------------------------------------------------
// <copyright file="GraphqlHttpGetTypedClient.cs" company="PraKrish Technologies">
//     Copyright (c) 2019 PraKrish Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>01/17/2019</date>
// <summary>The http get typed client</summary>
//-----------------------------------------------------------------------

namespace GraphqlClient.TypedClients
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using GraphqlClient.Reponse;

    /// <summary>
    /// Http typed client for get operation
    /// </summary>
    public class GraphqlHttpGetTypedClient: GraphqlTypedClientBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphqlHttpGetTypedClient.cs"/> class.
        /// </summary>
        /// <param name="client">The http client</param>
        public GraphqlHttpGetTypedClient(HttpClient client)
            : base(client)
        {
        }

        /// <summary>
        /// Send a GET request to the specified Uri
        /// </summary>
        /// <typeparam name="T">The type to which response would be deserialized</typeparam>
        /// <param name="query">Graphql query</param>
        /// <param name="url">Graphql api url</param>
        /// <returns>The graphql response object</returns>
        public async Task<IGraphqlResponse<T>> GetAsync<T>(string query, string url) 
            where T : class
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));
            if (string.IsNullOrEmpty(url)) throw new ArgumentException(nameof(url));

            var request = this.AddHttpRequestMessage(HttpMethod.Get, query, url);
            var response = await this.SendAsync<T>(request, CancellationToken.None).ConfigureAwait(false);
            return response;
        }

        /// <summary>
        /// Send a GET request to the specified Uri with cancellation token
        /// </summary>
        /// <typeparam name="T">The type to which response would be deserialized</typeparam>
        /// <param name="query">Graphql query</param>
        /// <param name="url">Graphql api url</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>The graphql response object</returns>
        public async Task<IGraphqlResponse<T>> GetAsync<T>(string query, string url,
            CancellationToken cancellationToken)
            where T : class
        {
            if (string.IsNullOrEmpty(query)) throw new ArgumentNullException(nameof(query));
            if (string.IsNullOrEmpty(url)) throw new ArgumentException(nameof(url));

            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException();
            }

            var request = this.AddHttpRequestMessage(HttpMethod.Get, query, url);
            var response = await this.SendAsync<T>(request, cancellationToken).ConfigureAwait(false);
            return response;
        }
    }
}
