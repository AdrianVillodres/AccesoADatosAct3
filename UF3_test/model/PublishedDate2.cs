using System;
using Newtonsoft.Json;
namespace UF3_test.model
{

    [Serializable]
    public class PublishedDate2
    {

        //[JsonProperty("date")]
        public String Date { get; set; }

        public override string ToString()
        {
            return
                "PublishedDate{" +
                "$Date = '" + Date + '\'' +
                "}";
        }
    }
}
