// ==========================================================================
// MongoApiResourceStore.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IdentityServer.MongoDb.dto;
using IdentityServer4.Models;
using MongoDB.Driver;

namespace IdentityServer.MongoDb
{
    public sealed class MongoApiResourceStore : MongoRepository<MongoApiResource, ApiResource>, IApiResourceStore
    {
        public MongoApiResourceStore(IMongoDatabase database) : base(database, "apiResources")
        {
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            FilterDefinition<MongoApiResource> query = Filters.Eq(x => x.Name, name);

            return SingleAsync(query);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var query = Filters .Where(a => scopeNames.Contains(a.Name));
            return ListAsync(query);
        }

        public Task<IEnumerable<ApiResource>> GetAllApiResources()
        {
            return ListAsync(Filters.Empty);
        }
    }
}