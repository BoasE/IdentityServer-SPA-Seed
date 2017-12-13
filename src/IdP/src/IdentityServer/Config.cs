using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Identity.Shared;
using IdentityServer;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;

namespace QuickstartIdentityServer
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("learning", "Learning API"),
                new ApiResource("statistics", "Statistics API"),
                new ApiResource("lessons", "Lessons API"),
                new ApiResource("soccerteam", "Soccer Team API"),
                new ApiResource("users", "User Management")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IConfigurationRoot config)
        {
            IEnumerable<IConfigurationSection> configs = config.GetSection("Identity:Clients").GetChildren();
            List<Client> result = configs.Select(ClientFactory.ClientFromSection).ToList();
            return result;
        }

        public static List<Scope> GetScopes()
        {
            return new List<Scope> {
               new Scope() {
                    Name = "users"
               }
           };

        }


        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }
            };
        }
    }
}