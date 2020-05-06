# Razor Page Web Chat Host
Add the Page folder and its contents to your bot and then:
* Replace the UseEndpoints statement in status.
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapRazorPages();
                });
* Add the following to ConfigureServices:
            // Add Razor Pages
            services.AddRazorPages();
* Add the following member to the Startup class
        public static HttpClient HttpClient = new HttpClient();
* Add you bot secret to the appsettings.cs config file