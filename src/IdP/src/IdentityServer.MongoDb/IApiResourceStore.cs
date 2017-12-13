// ==========================================================================
// IApiResourceStore.cs
// Bus Portal (busliniensuche.de)
// ==========================================================================
// All rights reserved.
// ========================================================================== 

using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace IdentityServer.MongoDb
{
    public interface IApiResourceStore
    {
        Task<ApiResource> FindApiResourceAsync(string name);

        Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames);

        Task<IEnumerable<ApiResource>> GetAllApiResources();
    }
}