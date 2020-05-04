using KMISApp.Model;
using Newtonsoft.Json;
using System;
using Microsoft.WindowsAzure.MobileServices;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace KMISApp.Services
{
    public class ApiServices
    {
        public async Task<bool> RegisterAsync(string email, string userName, string password, string confirmPassword)
        {
            bool Response = false;

            await Task.Run(() => 
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient client = new HttpClient(clientHandler);

                RegisterBindingModel model = new RegisterBindingModel
                {
                    Email = email,
                    UserName = userName,
                    Password = password,
                    ConfirmPassword = confirmPassword
                };

                string json = JsonConvert.SerializeObject(model);

                HttpContent content = new StringContent(json);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = client.PostAsync("https://192.168.100.85:45455/api/Account/Register", content);

                var mystring = response.GetAwaiter().GetResult();

                if (response.Result.IsSuccessStatusCode)
                {
                    Response = true;
                }
            });

            return Response;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var accessToken = string.Empty;

            await Task.Run(() => 
            {
                try
                {
                    var keyValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("username", email),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };

                    var request = new HttpRequestMessage(HttpMethod.Post, "https://192.168.100.85:45455/Token");

                    request.Content = new FormUrlEncodedContent(keyValues);

                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    HttpClient client = new HttpClient(clientHandler);

                    var response = client.SendAsync(request).Result;

                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync();
                        JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(json.Result);
                        var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
                        accessToken = jwtDynamic.Value<string>("access_token");

                        var Username = jwtDynamic.Value<string>("userName");
                        var AccessTokenExpirationDate = accessTokenExpiration;
                    }
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
            });
            return accessToken;
        }

        public async Task<string> UsernameAsync(string email, string password)
        {
            var Username = string.Empty;

            await Task.Run(() =>
            {
                try
                {
                    var keyValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("username", email),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };

                    var request = new HttpRequestMessage(HttpMethod.Post, "https://192.168.100.85:45455/Token");

                    request.Content = new FormUrlEncodedContent(keyValues);

                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    HttpClient client = new HttpClient(clientHandler);

                    var response = client.SendAsync(request).Result;

                    using (HttpContent content = response.Content)
                    {
                        var json = content.ReadAsStringAsync();
                        JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(json.Result);
                        var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
                        var accessToken = jwtDynamic.Value<string>("access_token");

                        Username = jwtDynamic.Value<string>("userName");
                        var AccessTokenExpirationDate = accessTokenExpiration;
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
            });
            return Username;
        }
    }
}
