using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace QueueByPassAPI.Adapter
{
    public class ApiClient
    {
        public async Task<string> PostCallBack(string url, string path, object data)
        {
            string request = url;

            if(!string.IsNullOrEmpty(path)) request = request.AppendPathSegment(path);

            var getData = await request
                .SetQueryParam("bypass",1)
                .PostJsonAsync(data)
                .ReceiveString();

            return getData;
        }

    }
}
