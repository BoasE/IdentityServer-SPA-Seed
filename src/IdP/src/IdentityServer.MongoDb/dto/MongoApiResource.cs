// ==========================================================================
// MongoApiResource.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System.Collections.Generic;
using IdentityServer4.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace IdentityServer.MongoDb.dto
{
    [BsonIgnoreExtraElements]
    public sealed class MongoApiResource
    {
        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public List<Secret> ApiSecrets { get; set; }

        public List<string> UserClaims { get; set; }

        public List<Scope> Scopes { get; set; }
    }
}