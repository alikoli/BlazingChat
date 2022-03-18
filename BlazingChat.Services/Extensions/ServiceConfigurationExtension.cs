namespace BlazingChat.Services.Extensions;

public static class ServiceConfigurationExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services,
            ApplicationSettings applicationSettings)
    {
        services.AddBlazoredToast();
        services.AddScoped<IShowToastService, ShowToastService>();
        return services;
    }
}
