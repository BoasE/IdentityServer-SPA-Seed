// ==========================================================================
// MongoGrantStore.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.MongoDb.dto;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using MongoDB.Driver;

namespace IdentityServer.MongoDb
{
    public sealed class MongoGrantStore : MongoRepository<MongoGrantDto, PersistedGrant>, IPersistedGrantStore
    {
        public MongoGrantStore(IMongoDatabase db) : base(db, "grants")
        {
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            MongoGrantDto dto = Mapper.Map<PersistedGrant, MongoGrantDto>(grant);
            return Collection.InsertOneAsync(dto);
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            return SingleAsync(Filters.Eq(x => x.Key, key));
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            FilterDefinition<MongoGrantDto> query = Filters.Eq(x => x.SubjectId, subjectId);
            List<MongoGrantDto> items = await Collection.Find(query).ToListAsync();

            return items
                .Select(Mapper.Map<MongoGrantDto, PersistedGrant>)
                .ToList();
        }

        public Task RemoveAsync(string key)
        {
            FilterDefinition<MongoGrantDto> query = Filters.Eq(x => x.Key, key);
            return Collection.DeleteManyAsync(key);
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            FilterDefinition<MongoGrantDto> query = Filters.And(
                Filters.Eq(x => x.SubjectId, subjectId),
                Filters.Eq(x => x.ClientId, clientId));

            return Collection.DeleteManyAsync(query);
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            FilterDefinition<MongoGrantDto> query = Filters.And(
                Filters.Eq(x => x.SubjectId, subjectId),
                Filters.Eq(x => x.ClientId, clientId),
                Filters.Eq(x => x.Type, type));

            return Collection.DeleteManyAsync(query);
        }
    }
}