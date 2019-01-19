//-----------------------------------------------------------------------
// <copyright file="IGraphqlResponse.cs" company="PraKrish Technologies">
//     Copyright (c) 2019 PraKrish Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>01/17/2019</date>
// <summary>Contract that defines response data</summary>
//-----------------------------------------------------------------------

namespace GraphqlClient.Reponse
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Interface that defines response type
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGraphqlResponse<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets or sets graphql response data
        /// </summary>
        TEntity Data { get; set; }

        /// <summary>
        /// Gets or sets the error occured on execution of the query
        /// </summary>
        IEnumerable<GraphqlError> Errors { get; set; }

        /// <summary>
        /// Gets has data flag, returns true if the response has data
        /// </summary>
        bool HasData { get; }

        /// <summary>
        /// Gets has error flag, returns true if the response has any error
        /// </summary>
        bool HasError { get; }

        /// <summary>
        /// Gets status code flag, returns true if the http status code between 200 and 300
        /// </summary>
        bool IsSuccessCode { get; }

        /// <summary>
        /// Gets or sets the http status code
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the time taken by operation in milli seconds
        /// </summary>
        long ElapsedTime { get; set; }
    }
}