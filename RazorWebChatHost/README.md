# Razor Page Web Chat Host
Add the `Page` folder and its contents to the project folder of your bot:

* Add the following member to the Startup class in startup.cs:
```c#
        public static HttpClient HttpClient = new HttpClient();
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

* Add you bot secret to the appsettings.cs config file
```json
  "BotSecret": "<your bot secret>",
```
