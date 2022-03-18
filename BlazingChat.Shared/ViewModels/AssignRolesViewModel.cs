using BlazingChat.Shared.Extensions;
using BlazingChat.Shared.Models;
using BlazingChat.Shared.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazingChat.ViewModels;

public class AssignRolesViewModel : IAssignRolesViewModel
{
    public IEnumerable<User> AllUsers { get; private set; } = new List<User>();
    public IEnumerable<User> FilteredUsers { get; private set; } = new List<User>();

    private readonly HttpClient _httpClient;
    private readonly IAccessTokenService _accessTokenService;
    private readonly IShowToastService _toastService;
    private string _searchString;

    public string SearchString
    {
        get { return _searchString; }
        set { 
            _searchString = value;
            ApplySearchFilter();
        }
    }


    public AssignRolesViewModel(HttpClient httpClient,
        IAccessTokenService accessTokenService, IShowToastService toastService
        )
    {
        _httpClient = httpClient;
        _accessTokenService = accessTokenService;
        _toastService = toastService;
    }

    public async Task LoadAllUsers()
    {
        var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
        AllUsers = await _httpClient.GetAsync<List<User>>("user/getallusers", jwtToken);
        FilteredUsers = AllUsers;
    }


    private void ApplySearchFilter()
    {
        if (string.IsNullOrEmpty(SearchString))
            FilteredUsers = AllUsers;
        else
            FilteredUsers = AllUsers.Where(u => u.EmailAddress.Contains(SearchString)).ToList();
    }

    public async Task AssignRole(long userId, string role)
    {
        foreach (var user in AllUsers)
        {
            if (user.UserId == userId && user.Role != role)
            {
                
                var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
                await _httpClient.PutAsync<int>("user/assignrole", new User { UserId = userId, Role = role }, jwtToken);
                if (role!="")
                    _toastService.ShowSuccess($"Role {role} assigned to {user.EmailAddress}", "");
                else
                    _toastService.ShowSuccess($"Role {user.Role} was removed from {user.EmailAddress}", "");
                user.Role = role;
                break;
            }
        }

    }

    public async Task DeleteUser(long userId)
    {
        foreach (var user in AllUsers)
        {
            if (user.UserId == userId)
            {

                var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
                int result = await _httpClient.DeleteAsync($"user/deleteuser/{userId}", jwtToken);
                    _toastService.ShowSuccess($"User {user.EmailAddress} was deleted", "");

                AllUsers = AllUsers.Where(u => u.UserId != userId).ToList();
                ApplySearchFilter();
                break;
            }
        }
    }
}
