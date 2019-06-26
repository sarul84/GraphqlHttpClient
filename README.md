# GraphqlHttpClient
Very simple Graphql http client for .net by taking advantage of IHttpClientFactory from latest version of .net core and standard

Build Status:

[![Build status](https://ci.appveyor.com/api/projects/status/u46ewl3ymt92poo7/branch/master?svg=true)](https://ci.appveyor.com/project/sarul84/graphqlhttpclient/branch/master)


Nuget package installation:
```
Install-Package Prakrishtha.GraphqlHttpClient -Version 1.0.0
```

The response object has status code, response time, error message (if any) properties which will be helpful when you interpret the result.

There is an extension method available to add Graphql client with inbuilt IoC container.

```
services.AddGraphqlHttpClient();
```

If you want to set base url and time out at the time of configuration, it can be achieved with the below code.

```
services.AddGraphqlHttpClient(new Uri("https://github.com"), new TimeSpan(0,0,20));
```
