// ==========================================================================
// MongoIdentityResourceStore.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.MongoDb.dto;
using IdentityServer4.Models;
using MongoDB.Driver;

namespace IdentityServer.MongoDb
{
    public sealed class MongoIdentityResourceStore : MongoRepository<MongoIdentityResource, IdentityResource>, IIdentityResourceStore
    {
        public MongoIdentityResourceStore(IMongoDatabase database) : base(database, "identityResources")
        {
        }

        public Task<IEnumerable<IdentityResource>> GetAllIdentityResources()
        {
            return ListAsync(Filters.Empty);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            FilterDefinition<MongoIdentityResource> query = Filters.Where(a => scopeNames.Contains(a.Name));

            return ListAsync(query);
        }
    }
}