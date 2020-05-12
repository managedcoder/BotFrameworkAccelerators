using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/* ----------------------------------------------------------------------------------
 * ToDo: Important tasks that must be complete for TokenController will work
 * The following tasks must be completed before running this code or it will fail:
 * 1) To avoid connection exhaustion issues, this code uses a single static instance
 *    of an HttpClient that needs to be added as a member of the Startup class:
 *    `public static HttpClient HttpClient = new HttpClient();`
 * 2) Change the fully qualified namespace of the reference to the HttpClient on line 42 
 *    of the TokenController constructor to match the namespace of your Startup class:
 *    `Client = <Your Namespace Here>.Startup.HttpClient;`
 * 2) This code expects that to get the DirectLine secret from the appsettings.json file so 
 *    you'll need to add the following setting to the appsettings.json file:
 *          `"directLineSecret": "<YourDirectLineSecret>",`
 * 3) Go to the Azure portal and enable the Direct Line channel and copy the Secret Key
 *    and paste it in as the value of the directLineSecret setting that you created in
 *    the previous step.
 * ----------------------------------------------------------------------------------
*/
namespace SecuredWebChatConrol.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public HttpClient Client { get; set; }

        public TokenController(IConfiguration configuration)
        {
            // If you are seeing a runtime error that complains about not being able to inject the
            // HttpClient, then see the note at the note at the top of this file
            Configuration = configuration;
            Client = Microsoft.BotBuilderSamples.Startup.HttpClient;
        }

        // GET: api/Token
        [HttpGet]
        [Produces("application/json")]
        async public Task<string> Get()
        {
            string generateTokenUri = "https://directline.botframework.com/v3/directline/tokens/generate";
            var directLineSecret = Configuration["directLineSecret"];

            return JsonConvert.SerializeObject(await GetTokenAsync(generateTokenUri, directLineSecret));
        }

        // GET: api/Token/5
        [HttpGet("{token}", Name = "Get")]
        [Produces("application/json")]
        async public Task<string> Get(string directLineSecret)
        {
            return JsonConvert.SerializeObject(await GetTokenAsync("https://directline.botframework.com/v3/directline/tokens/refresh", directLineSecret));
        }

        async Task<object> GetTokenAsync(string api, string directLineSecret)
        {
            dynamic response = new JObject();

            // Initialize the response object
            response.Token = "";
            response.UserId = "";
            response.IsSuccess = false;

            // Swap DirectLine secret for access token
            Client.DefaultRequestHeaders.Remove("Authorization");
            Client.DefaultRequestHeaders.Add("Authorization", $"Bearer {directLineSecret}");

            HttpResponseMessage directLineResponse = await Client.PostAsync(api, null);

            response.IsSuccess = directLineResponse.IsSuccessStatusCode;

            if ((bool)response.IsSuccess)
            {
                string body = directLineResponse.Content.ReadAsStringAsync().Result;

                response.Token = (string)JObject.Parse(body)["token"];
                response.UserId = $"dl_{Guid.NewGuid()}";
            }

            return response;
        }
    }
}
