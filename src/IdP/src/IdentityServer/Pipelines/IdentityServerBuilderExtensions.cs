using IdentityServer.MongoDb;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace QuickstartIdentityServer.Pipelines
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddMongoRepository(this IIdentityServerBuilder builder)
        {
            return builder;
        }


        public static IIdentityServerBuilder AddIdentityApiResources(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IResourceStore, CustomInMemoryResourceStore>();

            return builder;
        }

        public static IIdentityServerBuilder AddPersistedGrants(this IIdentityServerBuilder builder)
        {
            builder.Services.TryAddSingleton<IPersistedGrantStore, MongoGrantStore>();

            return builder;
        }
    }
}