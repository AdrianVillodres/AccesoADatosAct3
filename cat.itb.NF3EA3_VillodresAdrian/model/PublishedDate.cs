using System;
using Newtonsoft.Json;

namespace UF3_test.model
{

    [Serializable]
    public class PublishedDate
    {
        [JsonProperty("$date")]
        public String date { get; set; }

        public override string ToString()
        {
            return
                "PublishedDate{" +
                "$date = '" + date + '\'' +
                "}";
        }
    }
}
