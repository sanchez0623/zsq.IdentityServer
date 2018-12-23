using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace zsq.ClientApiUser
{
    class Program
    {
        static void Main(string[] args)
        {
            // “DiscoveryClient”已过时
            // var dis = DiscoveryClient.GetAsync("http://localhost:5002").Result;
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync("http://localhost:6230").Result;
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            // “TokenClient”已过时
            // var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            // var tokenResponse = tokenClient.RequestClientCredentialsAsync("api").Result;

            var tokenClient = new HttpClient();
            var tokenResponse = tokenClient.RequestTokenAsync(new TokenRequest
            {
                Address = disco.TokenEndpoint,
                GrantType = "client_credentials",
                ClientId = "client",
                ClientSecret = "secret",
                Parameters = {
                    { "scope", "api" }
                }
            }).Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            else
            {
                Console.WriteLine(tokenResponse.Json);
            }

            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            var httpResponse = httpClient.GetAsync("http://localhost:5002/api/values").Result;

            if (httpResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(httpResponse.Content.ReadAsStringAsync().Result);
            }

            Console.ReadLine();
        }
    }
}
