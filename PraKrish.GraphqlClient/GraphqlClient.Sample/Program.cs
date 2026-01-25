using GraphqlClient.Extensions;
using GraphqlClient.TypedClients;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace GraphqlClient.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

            Console.ReadKey();
        }

        public static async Task MainAsync(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddGraphqlHttpClient(new Uri("https://countries.trevorblades.com"), TimeSpan.FromSeconds(30))
                .BuildServiceProvider();

            var client = serviceProvider.GetService<GraphqlHttpTypedClient>();

            var queryString = "{ \"query\": \"query {\\n  countries\\n  {\\n    code,\\n    currency\\n  }\\n}\\n\" }";
            var response = await client.PostAsync<dynamic>(queryString, "");

            if(response?.HasError == true)
            {
                foreach (var error in response.Errors)
                {
                    Console.WriteLine(error.Message);
                }
            }
            else
            {
                Console.WriteLine("Elapsed Time in milli seconds, {0}", response?.ElapsedTime);
                Console.WriteLine(response?.Data);
            }
        }
    }
}
