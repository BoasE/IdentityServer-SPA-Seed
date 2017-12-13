using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Shared;
using IdentityModel;
using IdentityServer.MongoDb;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace QuickstartIdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly MongoUserStore store;

        public ProfileService(MongoUserStore store)
        {
            this.store = store;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var id = context.Subject.Claims.ToList().ResolveSubjectClaim();

            var user = await store.FindBySubjectId(id.Value);

            if (user != null)
            {
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Email, user.Email));
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Name, user.Username));
            }
        }


        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.FromResult(true);
        }
    }
}