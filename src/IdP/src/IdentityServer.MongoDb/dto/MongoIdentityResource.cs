// ==========================================================================
// MongoIdentityRessource.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace IdentityServer.MongoDb.dto
{
    [BsonIgnoreExtraElements]
    public sealed class MongoIdentityResource
    {
        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }
    
        public bool Required { get; set; }

        public bool Emphasize { get; set; }
   
        public bool ShowInDiscoveryDocument { get; set; }

        public List<string> UserClaims { get; set; }
    }
}