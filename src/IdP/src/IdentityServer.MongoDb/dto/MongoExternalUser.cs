using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IdentityServer.MongoDb.dto
{
    public sealed class MongoExternalUser
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonIgnore]
        public string Subject => Id.ToString();

        [BsonRequired]
        public string Username { get; set; }

        [BsonRequired]
        public string UsernameNormalized { get; set; }

        [BsonRequired]
        public string ProviderKey { get; set; }

        [BsonRequired]
        public string ProviderKeyNormalized { get; set; }

        [BsonRequired]
        public string ProviderId { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        [BsonRequired]
        public string EmailNormalized { get; set; }

        public List<MongoClaim> Claims { get; set; } = new List<MongoClaim>();
        public string Hash { get; set; }

        public void SetMongoInternals()
        {
            EmailNormalized = Normalize(Email);
            UsernameNormalized = Normalize(Username);
            ProviderKeyNormalized = Normalize(ProviderKey);
        }

        private static string Normalize(string source)
        {
            return source.ToLowerInvariant().Normalize();
        }
    }
}