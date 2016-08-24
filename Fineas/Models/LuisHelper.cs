using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Fineas.Models
{
    public class LuisHelper
    {
        public static string LuisModelId;
        public static string LuisApiKey;

        public static async Task<LuisResponse> ParseUserInput(string input)
        {
            string inputEscaped = Uri.EscapeDataString(input);

            using (var client = new HttpClient())
            {
                string uri = $"https://api.projectoxford.ai/luis/v1/application?id={LuisModelId}&subscription-key={LuisApiKey}&q={inputEscaped}";
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var luisResponse = JsonConvert.DeserializeObject<LuisResponse>(jsonResponse);
                    return luisResponse;
                }
            }

            return null;
        }
    }

    public class LuisResponse
    {
        public string Query { get; set; }
        public LuisIntent[] Intents { get; set; }
        public LuisEntity[] Entities { get; set; }
    }

    public class LuisIntent
    {
        public string Intent { get; set; }
        public float Score { get; set; }
    }

    public class LuisEntity
    {
        public string Entity { get; set; }
        public string Type { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public float Score { get; set; }
    }
}