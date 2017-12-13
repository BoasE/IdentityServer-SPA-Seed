// ==========================================================================
// MongoClientDto.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace IdentityServer.MongoDb.dto
{
    [BsonIgnoreExtraElements]
    public sealed class MongoClientDto
    {
        public bool LogoutSessionRequired { get; set; }

        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        public int IdentityTokenLifetime { get; set; }

        public int AccessTokenLifetime { get; set; }

        public int AuthorizationCodeLifetime { get; set; }

        public int AbsoluteRefreshTokenLifetime { get; set; }

        public int SlidingRefreshTokenLifetime { get; set; }

        public TokenUsage RefreshTokenUsage { get; set; }

        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        public TokenExpiration RefreshTokenExpiration { get; set; }

        public AccessTokenType AccessTokenType { get; set; }

        public bool EnableLocalLogin { get; set; }

        public List<string> IdentityProviderRestrictions { get; set; }

        public bool IncludeJwtId { get; set; }

        public List<Claim> Claims { get; set; }

        public bool AlwaysSendClientClaims { get; set; }

        public List<string> AllowedScopes { get; set; }

        public bool AllowOfflineAccess { get; set; }

        public List<string> AllowedCorsOrigins { get; set; }

        public string LogoutUri { get; set; }

        public bool Enabled { get; set; }

        public string ClientId { get; set; }

        public string ProtocolType { get; set; }

        public List<Secret> ClientSecrets { get; set; }

        public bool RequireClientSecret { get; set; }

        [BsonRequired]
        public string ClientName { get; set; }

        public string ClientUri { get; set; }

        public bool PrefixClientClaims { get; set; }

        public string LogoUri { get; set; }

        public bool AllowRememberConsent { get; set; }

        public IEnumerable<string> AllowedGrantTypes { get; set; }

        public bool RequirePkce { get; set; }

        public bool AllowPlainTextPkce { get; set; }

        public bool AllowAccessTokensViaBrowser { get; set; }

        public List<string> RedirectUris { get; set; }

        public List<string> PostLogoutRedirectUris { get; set; }

        public bool RequireConsent { get; set; }
    }
}