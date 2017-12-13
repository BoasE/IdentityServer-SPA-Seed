using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Shared;
using IdentityServer.MongoDb.dto;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace IdentityServer.MongoDb
{
    public sealed class MongoUserStore
    {
        private readonly IMongoDatabase db;
        private readonly IMongoCollection<MongoExternalUser> collection;
        private static readonly FilterDefinitionBuilder<MongoExternalUser> Filters = Builders<MongoExternalUser>.Filter;

        public MongoUserStore(IMongoDatabase db)
        {
            this.db = db;
            collection = db.GetCollection<MongoExternalUser>("Identity_Users");
        }

        public async Task<MongoExternalUser> AutoProvisionUser(string provider, string userId, List<Claim> claims)
        {
            var filter = Filters.Where(x=>x.UsernameNormalized==userId.ToLowerInvariant().Normalize());
            var numberOfUsersWithUsername = await collection.CountAsync(filter);
            if (numberOfUsersWithUsername > 0)
            {
                throw new UserExistsException();
            }
            
            
            var nameClaim = ClaimsFinder.ResolveNameClaim(userId, claims);
            var emailClaim = claims.ResolveEmailClaim();

            var user = new MongoExternalUser
            {
                ProviderId = userId,
                Username = nameClaim?.Value,
                Email = emailClaim.Value,
                ProviderKey = provider,
                Claims = claims.Select(x => new MongoClaim(x)).ToList()
            };
            user.SetMongoInternals();
            await collection.InsertOneAsync(user);

            return user;
        }

        public async Task<UpdateResult> SetPasswordHashForUser(MongoExternalUser user, string hash)
        {
            var update = Builders<MongoExternalUser>.Update
                .Set(s => s.Hash, hash);
            return await collection.UpdateOneAsync(x => x.Username == user.Username, update);
           
        }

        public Task<MongoExternalUser> FindByExternalProvider(string provider, string userId)
        {
            var filter = Filters.And(Filters.Eq(x => x.ProviderKey, provider), Filters.Eq(x => x.ProviderId, userId));
            return collection.Find(filter).SingleOrDefaultAsync();
        }

        public Task<MongoExternalUser> FindBySubjectId(string subjectId)
        {
            var id = ObjectId.Parse(subjectId);
            var filter = Filters.And(Filters.Eq(x => x.Id, id));

            return collection.Find(filter).SingleOrDefaultAsync();
        }

        public MongoExternalUser FindByUsername(string username)
        {
            var filter = Filters.Where(x=>x.UsernameNormalized==username.ToLowerInvariant().Normalize());
            return collection.Find(filter).SingleOrDefaultAsync().Result;
        }

        public bool ValidateCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            
            var passwordHasher = new PasswordHasher<MongoExternalUser>();
            
            var filter = Filters.Where(x=>x.UsernameNormalized==username.ToLowerInvariant().Normalize());
            var user = collection.Find(filter).SingleOrDefaultAsync().Result;
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Hash, password);
            return passwordVerificationResult == PasswordVerificationResult.Success;
        }
    }

    public class UserExistsException : Exception
    {
    }
}