using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HRInPocket.Clients.Base
{
    public abstract class BaseClient
    {
        protected HttpClient Client { get; }

        protected BaseClient(HttpClient Client) => Client = Client;
        
        
    }
}
