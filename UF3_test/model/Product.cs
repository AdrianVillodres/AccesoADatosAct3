using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace UF3_test.model
{
    [Serializable]
    public class Product
    {
        
        //[JsonProperty("name")]
        public string Name { get; set; }

        //[JsonProperty("price")]
        public int Price { get; set; }

        //[JsonProperty("stock")]
        public int Stock { get; set; }

        //[JsonProperty("picture")]
        public string Picture { get; set; }

       // [JsonProperty("categories")]
        public List<string> Categories { get; set; }
    }
}
