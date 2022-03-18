using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazingChat.Shared.Services
{
    public class WebAppAccessTokenService : IAccessTokenService
    {
        private readonly IJSRuntime _jsRuntime;

        public WebAppAccessTokenService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public async Task<string> GetAccessTokenAsync(string tokenName)
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", tokenName);
        }

        public async Task SetAccessTokenAsync(string tokenName, string tokenValue)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", tokenName, tokenValue);
        }

        public async Task RemoveAccessTokenAsync(string tokenName)
        {
            await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", tokenName);
        }

        public async Task<string> GetUserIdAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "userId");
        }

        public async Task SetUserIdAsync(string userId)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "userId", userId);
        }

        public async Task RemoveUserIdAsync()
        {
            await _jsRuntime.InvokeAsync<string>("localStorage.removeItem", "userId");
        }

    }
}
