using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UF3_test.model
{
    [Serializable]
    public class Product2
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        //[JsonProperty("name")]
        public string Name { get; set; }

       // [JsonProperty("price")]
        public int Price { get; set; }

       // [JsonProperty("stock")]
        public int Stock { get; set; }

       // [JsonProperty("picture")]
        public string Picture { get; set; }

       // [JsonProperty("categories")]
        public List<string> Categories { get; set; }
    }
}
