//-----------------------------------------------------------------------
// <copyright file="GraphqlClientBuilder.cs" company="PraKrish Technologies">
//     Copyright (c) 2019 PraKrish Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>01/17/2019</date>
// <summary>The extension class to add graphql client with asp.net core middleware</summary>
//-----------------------------------------------------------------------

namespace GraphqlClient.Extensions
{
    using GraphqlClient.TypedClients;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    /// <summary>
    /// Extension class for adding graphql client
    /// </summary>
    public static class GraphqlClientBuilder
    {
        /// <summary>
        /// Extension method to add graphql http client
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddGraphqlHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<GraphqlHttpTypedClient>();
            return services;
        }

        /// <summary>
        /// Extension method to add graphql http client with base Uri and timeout
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors</param>
        /// <param name="baseUri">Base Uri</param>
        /// <param name="timeout">Time out period</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddGraphqlHttpClient(this IServiceCollection services,
            Uri baseUri, TimeSpan timeout)
        {
            services.AddHttpClient<GraphqlHttpTypedClient>(client =>
            {
                client.BaseAddress = baseUri;
                client.Timeout = timeout;
            });
            return services;
        }
    }
}
