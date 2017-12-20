using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
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
            
            Console.WriteLine("Add new User");
            Console.WriteLine("Username");
            var username = Console.ReadLine();
            Console.WriteLine("Email");
            var email = Console.ReadLine();
            Console.WriteLine("Password");
            var password = Console.ReadLine();

            var user = new AddUserDto
            {
                Username = username,
                Password = password,
                Email = email
            };

            var response = await client.PostAsJsonAsync<AddUserDto>($"http://{HOST_IP}:5001/api/users", user);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error status code {response.StatusCode}");
                Console.WriteLine($"Error body {response.Content?.ReadAsStringAsync().Result}");
            }
            else
            {
                Console.WriteLine($"User created");
            }
        }
    }
    
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return httpClient.PostAsync(url, content);
        }

        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            var dataAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(dataAsString);
        }
    }


    public class AddUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}