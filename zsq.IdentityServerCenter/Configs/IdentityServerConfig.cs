using System.Collections.Generic;
using IdentityServer4.Models;

namespace zsq.IdentityServerCenter
{
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api","My Api")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //GrantType.ClientCredentials模式只能使用form-data
                new Client
                {
                    ClientId="client",
                    AllowedGrantTypes= { GrantType.ClientCredentials},
                    ClientSecrets={ new Secret("secret".Sha256())},
                    AllowedScopes={"api"}//这个client允许访问的apiResource（对应apiResource的名称）
                }
            };
        }
    }
}