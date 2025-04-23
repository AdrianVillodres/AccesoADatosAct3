using System;
using MongoDB.Driver;

namespace UF3_test.connections
{
    public class MongoClusterConnection
    {
        private static String URL = "mongodb+srv://itb:itb@cluster0.eldyq.mongodb.net/?retryWrites=true&w=majority";
        public static IMongoDatabase GetDatabase(String database)
        {
            MongoClient dbClient = new MongoClient(URL);
            return dbClient.GetDatabase(database);
        }

        public static MongoClient GetMongoClient()
        {
            return new MongoClient(URL);
        }
    }
}