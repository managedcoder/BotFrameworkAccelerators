# Razor Page Web Chat Host

This Razor page shows how to securly hosts a Web Chat control and it can be used as a convenient
test page during development that other can use to access your bot.  To use this page, do the
following:
    
* Add the `Pages` folder and its contents to the project folder of your bot:

* Add the following member to the Startup class in startup.cs:
```c#
public static HttpClient HttpClient = new HttpClient();
```

* Add a using statement to Index.cshtml.cs that resolves the Startup.HttpClient reference
```
using <namespace to your Startup class>;
```

* Add the following to ConfigureServices() method in startup.cs:
```c#
// Add Razor Pages
services.AddRazorPages();
```

* Replace the UseEndpoints statement in Configure() method in startup.cs:
```c#
.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});
```
* Add the Direct Line Channel to your bot in the Azure portal and use it's bot secret to set 
the BotSecret configuration setting in your appsettings.json file
```json
"BotSecret": "<your bot secret>",
```

* Take a look at the IndexModel() constructor in the code behind to see which other settings
can be configured, if desired

* Optional - Delete the `default.htm` file from the `wwwroot` folder since you now have a new home page

* Note: Unlike other approaches, this Razor page DOES NOT require a token controller API to be
added to your solution since that functionality is taken care of in the code behind for this page.

Details regarding securing web chat control can be found [here](https://docs.microsoft.com/en-us/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-authentication?view=azure-bot-service-4.0)

