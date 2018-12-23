using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace zsq.PasswordApiUser
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync("http://localhost:6230").Result;
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
            }

            var tokenClient = new HttpClient();
            var tokenResponse = tokenClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "pwdClient",
                ClientSecret = "secret",
                UserName = "sanchez",
                Password = "123456",
                Scope = "api"
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
