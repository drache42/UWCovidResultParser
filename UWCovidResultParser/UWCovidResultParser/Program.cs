using ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace UWCovidResultParser
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HttpClient client = new HttpClient();

            // These values return a real result
            var values = new Dictionary<string, string>
            {
                { "barcode", "QRNJZEWDN8TGDTJD" },
                { "dob", "03/16/1984" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://securelink.labmed.uw.edu/result", content);
            var responseString = await response.Content.ReadAsStringAsync();

            responseString.Dump();
        }
    }
}
