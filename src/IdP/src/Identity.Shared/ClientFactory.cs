using System;
using System.Linq;
using IdentityServer;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace Identity.Shared
{
    public static class ClientFactory
    {
        /// <seealso href="//https://github.com/IdentityServer/IdentityServer4/blob/dev/src/IdentityServer4/Models/GrantTypes.cs"/>
        public static Client ClientFromSection(this IConfigurationSection section)
        {
            string id = section["ClientId"];
            string name = section["ClientName"];
            Console.WriteLine($"Adding client - {id} - {name}");

            var client = new Client
            {
                ClientId = id,
                ClientName = name,
                AllowedGrantTypes = section.GetStringArray("AllowedGrants").ToList(),

                ClientSecrets =
                {
                    new Secret(section["Secret"].Sha256())
                },

                RedirectUris = section.GetStringArray("RedirectUris"),
                PostLogoutRedirectUris = section.GetStringArray("PostLogoutRedirectUris"),

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email
                },
                AllowOfflineAccess = bool.Parse(section["AllowOfflineAccess"] ?? "true"),
                AllowAccessTokensViaBrowser = true,
                RequireConsent = bool.Parse(section["RequireConsent"] ?? "false"),
                AlwaysIncludeUserClaimsInIdToken = bool.Parse(section["AlwaysIncludeUserClaimsInIdToken"] ?? "false"),
                EnableLocalLogin = bool.Parse(section["EnableLocalLogin"] ?? "false")
            };

            var cors = section.GetStringArray("AllowedCorsOrigins");
            if (!cors.IsNullOrEmpty())
            {
                client.AllowedCorsOrigins = cors;
            }

            ApplyAdditionalClientScopes(section, client);

            return client;
        }

        private static void ApplyAdditionalClientScopes(IConfiguration section, Client client)
        {
            var scopes = section.GetStringArray("AllowedScopes");

            if (scopes.IsNullOrEmpty()) return;

            foreach (var scope in scopes)
            {
                client.AllowedScopes.Add(scope);
            }
        }
    }
}