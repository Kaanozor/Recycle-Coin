using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Helpers
{
    public static class NodejsHelper
    {
        public static async Task<List<dynamic>> GetBlocks()
        {
            var api = "http://localhost:3000/transaction/getall";
            var client = new HttpClient();
            var results = await client.GetStringAsync(api);
            var result = JsonConvert.DeserializeObject<List<ExpandoObject>>(results);
            return result.Cast<dynamic>().ToList();
        }
        public static async Task<decimal> GetAmount(string publickey)
        {
            var api = "http://localhost:3000/amount/"+publickey;
            var client = new HttpClient();
            var results = await client.GetStringAsync(api);
            var result = JsonConvert.DeserializeObject<decimal>(results);
            return result;
        }
    }
}
