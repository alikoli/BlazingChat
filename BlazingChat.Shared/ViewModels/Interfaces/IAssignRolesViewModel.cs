using BlazingChat.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazingChat.ViewModels
{
    public interface IAssignRolesViewModel
    {
        public IEnumerable<User> AllUsers { get; }
        public IEnumerable<User> FilteredUsers { get; }
        public string SearchString { get; set; }

        public Task LoadAllUsers();
        public Task AssignRole(long userId, string role);
        public Task DeleteUser(long userId);
    }
}
