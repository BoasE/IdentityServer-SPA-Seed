using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServer.MongoDb
{
    public interface IIdentityResourceStore
    {
        Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames);

        Task<IEnumerable<IdentityResource>> GetAllIdentityResources();
    }
}
