# GraphqlHttpClient
Very simple Graphql http client for .net by taking advantage of IHttpClientFactory from latest version of .net core and standard

Build Status:

[![Build status](https://ci.appveyor.com/api/projects/status/u46ewl3ymt92poo7/branch/master?svg=true)](https://ci.appveyor.com/project/sarul84/graphqlhttpclient/branch/master)


Nuget package installation:
```
Install-Package Prakrishtha.GraphqlHttpClient -Version 1.0.0
```

The response object has Data, status code, response time, error message (if any) properties which will be helpful when you interpret the result.

```
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
```

There is an extension method available to add Graphql client with inbuilt IoC container.

```
services.AddGraphqlHttpClient();
```

If you want to set base url and time out at the time of configuration, it can be achieved with the below code.

```
services.AddGraphqlHttpClient(new Uri("https://github.com"), new TimeSpan(0,0,20));
```
