# Razor Page Web Chat Host

This Razor page shows how to securly hosts a Web Chat control and it can be used as a convenient
test page during development that others can use to access your bot.  To use this page, do the
following:
    
1) Add the `Pages` folder and its contents to your bot project (could be added to your assistant or skill or both)
2) Add the following member to the Startup class in startup.cs:
```c#
public static HttpClient HttpClient = new HttpClient();
```
3) Add a using statement to Index.cshtml.cs that resolves the Startup.HttpClient reference
```
using <namespace to your Startup class>;
```
4) Add the following to ConfigureServices() method in startup.cs:
```c#
// Add Razor Pages
services.AddRazorPages();
```
5) Replace the UseEndpoints statement in Configure() method in startup.cs:
```c#
.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});
```
6) Add the Direct Line Channel to your bot in the Azure portal and use it's bot secret to set 
the BotSecret configuration setting in your appsettings.json file
```json
"BotSecret": "<your bot secret>",
```
7) Take a look at the IndexModel() constructor in the code behind to see which other settings
can be configured, if desired
8) Optional - Delete the `default.htm` file from the `wwwroot` folder since you now have a new home page

**Note:** Unlike other approaches, this Razor page DOES NOT require a token controller API to be
added to your solution since that functionality is taken care of in the code behind for this page.

**Note:** Its important to understand that when this page runs in localhost it will be interacting with
the deployed bot, **NOT THE BOT RUNNING IN LOCAL HOST**.  So you can't use it for localhost debugging
to hit breakpoints in your code unless you create a separate bot channels registration that points to
an ngrok-exposed localhost.

If you want to use the this page to drive localhost debugging via a separate Bot Channel Registration,
install ngrok and run this ngrok command
```
ngrok http 3978 -host-header=localhost:3978
```
When creating the Bot Channel Registration, you'll have to set the messaging endpoint of the Bot Channel
Registration to the https version of the endpoints exposed by ngrok which should look something like this:
```
https://<ngrok generated #>.ngrok.io/api/messages
```

Details regarding securing web chat control can be found [here](https://docs.microsoft.com/en-us/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-authentication?view=azure-bot-service-4.0)

