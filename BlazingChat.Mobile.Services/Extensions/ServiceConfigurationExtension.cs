namespace BlazingChat.Mobile.Services.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,
            ApplicationSettings applicationSettings)
    {
        services.AddIonicToast();
        services.AddScoped<IShowToastService, ShowToastService>();
        return services;
    }
}
