using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Ionic.Toast
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIonicToast(this IServiceCollection services)
        {
            return services.AddScoped<IToastService, ToastService>();
        }
    }
}
