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
        //metode per carregar la colecció
        public void LoadProductsCollection()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("products");

            var collection = database.GetCollection<Product>("products");

            FileInfo file = new FileInfo("../../../files/products.json");

            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Product product = JsonConvert.DeserializeObject<Product>(line);

                    if (ObjectId.TryParse(product.Id, out var objectId))
                    {
                        product.Id = objectId.ToString();
                    }
                    else
                    {
                        product.Id = ObjectId.GenerateNewId().ToString();
                    }

                    collection.InsertOne(product);
                }
            }
        }

        //metode per seleccionar el producte més car
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

            Console.WriteLine($"{product.name}, {product.price}");
        }

        //metode mer comptar la suma de tots els stocks
        public void CountSumProducts()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var productsC = database.GetCollection<Product>("products");

            var totalStock = productsC.AsQueryable<Product>()
                                      .Sum(p => p.stock);

            Console.WriteLine($"La suma total de los stocks es: {totalStock}");
        }

    }

}
