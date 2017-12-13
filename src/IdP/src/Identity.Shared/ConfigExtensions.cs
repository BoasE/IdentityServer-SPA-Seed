using Microsoft.Extensions.Configuration;

namespace IdentityServer
{
    public static class ConfigExtensions
    {
        public static string[] GetStringArray(this IConfiguration section, string key)
        {
            return section.GetSection(key).Get<string[]>();
        }
    }
}