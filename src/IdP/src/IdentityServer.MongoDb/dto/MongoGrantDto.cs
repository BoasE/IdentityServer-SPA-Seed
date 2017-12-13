// ==========================================================================
// MongoGrantDto.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System;
using MongoDB.Bson.Serialization.Attributes;

namespace IdentityServer.MongoDb.dto
{
    [BsonIgnoreExtraElements]
    public sealed class MongoGrantDto
    {
        public string Key { get; set; }

        public string Type { get; set; }

        public string SubjectId { get; set; }

        public string ClientId { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? Expiration { get; set; }

        public string Data { get; set; }
    }
}