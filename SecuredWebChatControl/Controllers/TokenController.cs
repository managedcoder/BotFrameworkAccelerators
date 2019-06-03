using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using MyFirstVirtualAssistantRHW.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/* ----------------------------------------------------------------------------------
 * Important!!!
 * There are two things you must do before running this code:
 * 1) To avoid connection exhaustion issues, this code exercises the best practice of
 *    only using a single HttpClient instance per application so you'll need to add 
 *    dependency injection by adding the follwing line of code to the Startup.cs file 
 *    somewhere in the ConfigureServices() method:
 *          services.AddSingleton<HttpClient>(new HttpClient());
 * 2) This code expects that to get the Bot Secret from the appsettings.cs file so 
 *    you'll need to add the following setting to the appsettings.cs file:
 *          "directLineSecret": "<YourDirectLineSecret>",
 * ----------------------------------------------------------------------------------
*/
namespace MyFirstVirtualAssistantRHW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public HttpClient Client { get; set; }

        public TokenController(IConfiguration configuration, HttpClient httpClient)
        {
            // If you are seeing a runtime error that complains about not being able to inject the
            // HttpClient, then see the note at the note at the top of this file
            Configuration = configuration;
            Client = httpClient;
        }

        // GET: api/Token
        [HttpGet]
        async public Task<string> Get()
        {
            string generateTokenUri = "https://directline.botframework.com/v3/directline/tokens/generate";
            var directLineSecret = Configuration["directLineSecret"];

            return await GetTokenAsync(generateTokenUri, directLineSecret);
        }

        // GET: api/Token/5
        [HttpGet("{token}", Name = "Get")]
        async public Task<string> Get(string token)
        {
            return await GetTokenAsync("https://directline.botframework.com/v3/directline/tokens/refresh", token);
        }

        async Task<string> GetTokenAsync(string api, string bearerValue)
        {
            Client.DefaultRequestHeaders.Remove("Authorization");
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerValue}");

            HttpResponseMessage response = await Client.PostAsync(api, null);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string body = response.Content.ReadAsStringAsync().Result;

                return (string) JObject.Parse(body)["token"];
            }
            else
                return null;
        }
    }
}
