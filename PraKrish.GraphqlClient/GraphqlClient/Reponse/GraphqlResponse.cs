//-----------------------------------------------------------------------
// <copyright file="GraphqlResponse.cs" company="PraKrish Technologies">
//     Copyright (c) 2019 PraKrish Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>01/17/2019</date>
// <summary>Class file that returns response data</summary>
//-----------------------------------------------------------------------

namespace GraphqlClient.Reponse
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// The graphql response class that returns once query execution is over
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GraphqlResponse<TEntity> : IGraphqlResponse<TEntity> where TEntity: class
    {
        /// <summary>
        /// Gets or sets graphql response data
        /// </summary>
        public TEntity Data { get; set; }

        /// <summary>
        /// Gets or sets the error occured on execution of the query
        /// </summary>
        public IEnumerable<GraphqlError> Errors { get; set; }

        /// <summary>
        /// Gets or sets the http status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the time taken by the operation in milli seconds
        /// </summary>
        public long ElapsedTime { get; set; }

        /// <summary>
        /// Gets has data flag, returns true if the response has data
        /// </summary>
        public bool HasData => Data != null;

        /// <summary>
        /// Gets has error flag, returns true if the response has any error
        /// </summary>
        public bool HasError => Errors?.Any() ?? false;

        /// <summary>
        /// Gets status code flag, returns true if the http status code between 200 and 300
        /// </summary>
        public bool IsSuccessCode => (int)StatusCode >= 200 && (int)StatusCode < 300;
    }
}
