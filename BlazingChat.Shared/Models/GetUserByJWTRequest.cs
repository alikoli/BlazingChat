using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazingChat.Shared
{
    public class GetUserByJWTRequest
    {
        public string Token { get; set; }
    }
}
