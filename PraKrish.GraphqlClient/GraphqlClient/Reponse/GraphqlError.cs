//-----------------------------------------------------------------------
// <copyright file="GraphqlError.cs" company="PraKrish Technologies">
//     Copyright (c) 2019 PraKrish Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>01/17/2019</date>
// <summary>Class file that defines error message</summary>
//-----------------------------------------------------------------------

namespace GraphqlClient.Reponse
{
    /// <summary>
    /// Graphql error message class
    /// </summary>
    public class GraphqlError
    {
        /// <summary>
        /// Gets or sets description of the error
        /// </summary>
        public string Message { get; set; }
    }
}
