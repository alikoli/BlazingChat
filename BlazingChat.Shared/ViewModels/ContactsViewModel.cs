using BlazingChat.Shared.Extensions;
using BlazingChat.Shared.Models;
using BlazingChat.Shared.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazingChat.ViewModels
{
    public class ContactsViewModel : IContactsViewModel
    {
        public IEnumerable<Contact> Contacts { get; private set; } = new List<Contact>();
        public int ContactsCount { get; private set; } = 1;

        private int _startIndex;
        private int _count;

        private string _searchString;

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                LoadOnlyVisibleContactsDB(_startIndex, _count);
                LoadContactsCountDB();
            }
        }


        private readonly HttpClient _httpClient;
        private readonly IAccessTokenService _accessTokenService;

        public ContactsViewModel(HttpClient httpClient,
            IAccessTokenService accessTokenService)
        {
            _httpClient = httpClient;
            _accessTokenService = accessTokenService;
        }

        private void LoadCurrentObject(IEnumerable<User> users) =>
            Contacts = users.Select(u => (Contact)u);

        //loading actual contacts
        public async Task LoadAllContactsDB()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            var users = await _httpClient.GetAsync<List<User>>("contacts/getcontacts", jwtToken);
            LoadCurrentObject(users);
        }

        public async Task LoadOnlyVisibleContactsDB(int startIndex, int count)
        {
            _startIndex = startIndex;
            _count = count;
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            var users = await _httpClient.GetAsync<List<User>>($"contacts/getvisiblecontacts?startIndex={startIndex}&count={count}&filter={SearchString}", jwtToken);
            LoadCurrentObject(users);
        }

        public async Task LoadContactsCountDB()
        {
            var jwtToken = await _accessTokenService.GetAccessTokenAsync("jwt_token");
            ContactsCount = await _httpClient.GetAsync<int>($"contacts/getcontactscount?filter={SearchString}", jwtToken);
        }

    }
}
