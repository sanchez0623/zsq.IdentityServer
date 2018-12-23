using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

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
                },
                new Client
                {
                    ClientId="pwdClient",
                    AllowedGrantTypes= { GrantType.ResourceOwnerPassword},
                    ClientSecrets={ new Secret("secret".Sha256())},
                    RequireClientSecret=false,//设置为false，在请求时就不用添加secret参数
                    AllowedScopes={"api"}//这个client允许访问的apiResource（对应apiResource的名称）
                }
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>{
                new TestUser{
                    SubjectId="1",
                    Username="sanchez",
                    Password="123456"
                }
            };
        }
    }
}