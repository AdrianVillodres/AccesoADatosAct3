using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json;
using UF3_test.connections;
using UF3_test.model;

namespace cat.itb.NF3EA3_VillodresAdrian.cruds
{
    public class ProductsCRUD
    {
        public void LoadProductsCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("products");
            var collection = database.GetCollection<BsonDocument>("products");

            FileInfo file = new FileInfo("../../../files/products.json");

            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Product product = JsonConvert.DeserializeObject<Product>(line);
                    Console.WriteLine(product.name);
                    string json = JsonConvert.SerializeObject(product);
                    var document = new BsonDocument();
                    document.Add(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            }
        }
    }
}
