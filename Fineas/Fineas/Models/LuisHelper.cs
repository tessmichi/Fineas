// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

namespace Fineas.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class LuisHelper
    {
        public static string LuisModelId;
        public static string LuisApiKey;

        public static async Task<LuisResponse> ParseUserInput(string input)
        {
            string inputEscaped = Uri.EscapeDataString(input);

            using (var client = new HttpClient())
            {
                // TODO: move to config file
                string uri = $"https://api.projectoxford.ai/luis/v1/application?id={LuisModelId}&subscription-key={LuisApiKey}&q={inputEscaped}";
                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var luisResponse = JsonConvert.DeserializeObject<LuisResponse>(jsonResponse);
                    return luisResponse;
                }
            }

            return LuisResponse.Unknown;
        }
    }

    public class LuisResponse
    {
        public static LuisResponse Unknown = new LuisResponse();

        public string Query { get; set; }
        public LuisIntent[] Intents { get; set; }
        public LuisEntity[] Entities { get; set; }

        public LuisIntent GetBestIntent()
        {
            return Intents.Aggregate((currMax, x) => (currMax == null || x.Score > currMax.Score ? x : currMax));
        }
    }

    public class LuisIntent
    {
        // "unkown" is covered by Intent = "None"

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

/*

MIT License

Copyright (c) 2016 Tess

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/
