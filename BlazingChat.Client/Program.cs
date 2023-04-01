var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

#region ConfigureServicesTest

// setting client host environment 
builder.Services.AddSingleton<IHostingEnvironment>(
    new HostingEnvironment() { EnvironmentName = builder.HostEnvironment.Environment });

// adding client app settings 
var applicationSettingsSection = builder.Configuration.GetSection("ApplicationSettings");
builder.Services.Configure<ApplicationSettings>(options =>
{
    applicationSettingsSection.Bind(options);
});

// adding application services
builder.Services.AddBlazingChat(applicationSettingsSection.Get<ApplicationSettings>());
builder.Services.AddAppServices(applicationSettingsSection.Get<ApplicationSettings>());

#endregion

await builder.Build().RunAsync();
