using System;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace IdentityServer.UserClient {
    class Program {


        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

            Console.ReadKey();
        }

        public static async Task MainAsync(string[] args)
        {
           // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync ("http://localhost");
            if (disco.IsError) {
                Console.WriteLine (disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint,"user-setup", "users1234");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync ("users");

            if (tokenResponse.IsError) {
                Console.WriteLine (tokenResponse.Error);
                return;
            }

            Console.WriteLine (tokenResponse.Json);
        }
    }
}