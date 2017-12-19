using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace IdentityServer.UserClient {
    class Program {


        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();

            Console.ReadKey();
        }

        public static async Task MainAsync(string[] args)
        {
            var HOST_IP = Environment.GetEnvironmentVariable("HOST_IP");
            
           // discover endpoints from metadata
            var formattableString = $"http://{HOST_IP}";
            Console.WriteLine(formattableString);
            var discoveryClient = new DiscoveryClient(formattableString);
            discoveryClient.Policy.RequireHttps = false;
            var disco = await discoveryClient.GetAsync();
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
            
            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync($"http://{HOST_IP}:5001/api/users");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"status {response.StatusCode}");
            }
            else
            {
                Console.WriteLine($"API Status Code {response.StatusCode}");
//                var content = await response.Content.ReadAsStringAsync();
//                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}