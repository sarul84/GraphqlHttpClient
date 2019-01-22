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

            var queryString = "{\"query\":{countries {code,currency}}}";
            var response = await client.PostAsync<dynamic>(queryString, "");

            if(response?.HasError == true)
            {
                Console.WriteLine(response?.Errors?.GetEnumerator()?.Current.Message);
            }
            else
            {
                Console.WriteLine(response?.Data);
            }
        }
    }
}
