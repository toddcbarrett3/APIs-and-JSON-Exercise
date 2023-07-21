using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace APIsAndJSON
{
    public static class RonVSKanyeAPI
    {
       public static void RonKanyeConversation()
       {
            var client = new HttpClient();

            for (int i = 0; i < 10; i++)
            {
                string ronUrl = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";

                var ronResponse = client.GetStringAsync(ronUrl).Result;

                var ronQuote = JArray.Parse(ronResponse);

                Console.WriteLine($"Ron: {ronQuote[0]}");
                Console.WriteLine();

                string kanyeUrl = "https://api.kanye.rest/";

                var kanyeResponse = client.GetStringAsync(kanyeUrl).Result;

                var kanyeQuote = JObject.Parse(kanyeResponse);

                Console.WriteLine($"Kanye: {kanyeQuote["quote"]}");
                Console.WriteLine();
            }
       }
    }
    

}
