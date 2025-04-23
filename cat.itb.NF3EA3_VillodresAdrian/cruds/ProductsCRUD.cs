using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using UF3_test.connections;
using UF3_test.model;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

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

        public void SelectByExpesivePrice()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var productsC = database.GetCollection<Product>("products");
            var query = productsC.AsQueryable<Product>();
            var expensiveproduct =
                query
                .Select(p => p.price)
                .Max();
            var product =
                query
                .Where(p => p.price == expensiveproduct)
                .Single(); 
            
                Console.WriteLine(product.name, product.price);
        }
    }
}
