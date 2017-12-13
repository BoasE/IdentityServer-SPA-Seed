using System.Security.Claims;

namespace IdentityServer.MongoDb.dto
{
    public sealed class MongoClaim
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }

        public string Issuer { get; set; }

        public MongoClaim()
        {
        }

        public MongoClaim(Claim claim)
        {
            Name = claim.Type;
            Value = claim.Value;
            ValueType = claim.ValueType;
            Issuer = claim.Issuer;
        }

        public Claim ToClaim()
        {
            return new Claim(Name, Value, ValueType, Issuer);
        }
    }
}