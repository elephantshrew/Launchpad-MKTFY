using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Launchpad.Auth.Config
{
    public static class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static List<TestUser> GetUsers() =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "William",
                    Password = "WilliamPassword"
            },

            new TestUser
            {
                SubjectId = "2",
                Username = "Amy",
                Password = "AmyPassword"
            }
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                        new ApiResource
                        {
                            Name = "launchpadapi",
                            DisplayName = "Launchpad API",
                            Scopes = { "launchpadapi.scope", "launchpadapi" }
                        }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("launchpadapi.scope", "Launchpad API")
            };
        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "CompanyEmployee",
                    ClientSecrets = new [] { new Secret("WilliamSecret".Sha512())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { "Launchpadapi.scope", IdentityServerConstants.StandardScopes.OpenId}
                }

            };

    }

}
