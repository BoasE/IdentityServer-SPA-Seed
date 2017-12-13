using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace QuickstartIdentityServer.Pipelines
{
    public class CustomInMemoryResourceStore : IResourceStore
    {
        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            IEnumerable<IdentityResource> result = Config.GetIdentityResources().Where(x => scopeNames.Contains(x.Name));
            return Task.FromResult(result);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            IEnumerable<ApiResource> result = Config.GetApiResources().Where(x => scopeNames.Contains(x.Name));
            return Task.FromResult(result);
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            ApiResource result = Config.GetApiResources().SingleOrDefault(x => x.Name.Equals(name));
            return Task.FromResult(result);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            var result = new Resources(Config.GetIdentityResources(), Config.GetApiResources());
            return Task.FromResult(result);
        }

        public Task<Resources> GetAllResources()
        {
            var result = new Resources(Config.GetIdentityResources(), Config.GetApiResources());
            return Task.FromResult(result);
        }
    }
}
