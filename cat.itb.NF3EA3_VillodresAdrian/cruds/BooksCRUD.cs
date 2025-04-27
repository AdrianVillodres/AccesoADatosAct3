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

namespace cat.itb.NF3EA3_VillodresAdrian.cruds
{
    public class BooksCRUD
    {
        public void LoadBooksCollection()
        {
            FileInfo file = new FileInfo("../../../files/books.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();

            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("books");
            var collection = database.GetCollection<BsonDocument>("books");

            if (books != null)
            {
                foreach (var book in books)
                {
                    Console.WriteLine(book.title);
                    string json = JsonConvert.SerializeObject(book);
                    var document = BsonDocument.Parse(json);
                    collection.InsertOne(document);
                }
            }
        }

        public void ShowBooksByPagesAndCategory(int minPages, int maxPages, string category)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var books = collection.AsQueryable()
                .Where(b => b["pageCount"].AsInt32 >= minPages
                         && b["pageCount"].AsInt32 <= maxPages
                         && b["categories"].AsBsonArray.Contains(category))
                .ToList();

            foreach (var book in books)
            {
                string title = book["title"].AsString;
                string authors = string.Join(", ", book["authors"].AsBsonArray.Select(a => a.AsString));
                int pageCount = book["pageCount"].AsInt32;

                Console.WriteLine($"Titol: {title}");
                Console.WriteLine($"Autors: {authors}");
                Console.WriteLine($"Num. Pagines: {pageCount}");
                Console.WriteLine("------------------------------------");
            }
        }

        public void SelectByAuthors(string[] authors)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var filtered = collection
                .Find(FilterDefinition<BsonDocument>.Empty)
                .ToList()
                .Where(book => authors.All(a => book["authors"]
                    .AsBsonArray
                    .Any(author => author.AsString == a)))
                .ToList();

            foreach (var book in filtered)
            {
                Console.WriteLine($"Titol:   {book["title"].AsString}");
                Console.WriteLine($"Autors: {string.Join(", ", book["authors"].AsBsonArray.Select(a => a.AsString))}");
                Console.WriteLine("------------------------------------");
            }
        }


        public void SelectBooksTitleAndStatus()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var books = collection
                .Find(FilterDefinition<BsonDocument>.Empty)
                .ToList()
                .Select(book => new
                {
                    Title = book["title"].AsString,
                    Status = book["status"].AsString
                })
                .ToList();

            foreach (var b in books)
            {
                Console.WriteLine($"Titol: {b.Title}");
                Console.WriteLine($"Estat: {b.Status}");
                Console.WriteLine("------------------------------------");
            }
        }
        public void SelectBooksOrderByPages()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var books = collection
                .Find(FilterDefinition<BsonDocument>.Empty)
                .ToList()
                .OrderByDescending(b => b["pageCount"].AsInt32)
                .Select(b => new
                {
                    Title = b["title"].AsString,
                    Categories = string.Join(", ",
                        b["categories"].AsBsonArray.Select(c => c.AsString))
                })
                .ToList();

            foreach (var b in books)
            {
                Console.WriteLine($"Titol:      {b.Title}");
                Console.WriteLine($"Categories: {b.Categories}");
                Console.WriteLine("------------------------------------");
            }
        }

        public void SelectBooksByAuthor(string author)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var books = collection
                .Find(FilterDefinition<BsonDocument>.Empty)
                .ToList()
                .Where(book => book["authors"]
                    .AsBsonArray
                    .Any(a => a.AsString == author))
                .Select(book => new
                {
                    Title = book["title"].AsString,
                    Authors = string.Join(", ", book["authors"].AsBsonArray.Select(a => a.AsString))
                })
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"Títol: {book.Title}");
                Console.WriteLine($"Autors: {book.Authors}");
                Console.WriteLine("------------------------------------");
            }
        }

        public void SelectBooksByCategoryExcludingAuthor(string category, string excludedAuthor)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("books");

            var books = collection
                .Find(FilterDefinition<BsonDocument>.Empty)
                .ToList()
                .Where(b => b["categories"].AsBsonArray.Any(c => c.AsString == category)
                         && !b["authors"].AsBsonArray.Any(a => a.AsString == excludedAuthor))
                .OrderBy(b => b["title"].AsString);

            foreach (var book in books)
            {
                Console.WriteLine($"Títol: {book["title"].AsString}");
                Console.WriteLine($"Categories: {string.Join(", ", book["categories"].AsBsonArray.Select(c => c.AsString))}");
                Console.WriteLine("------------------------------------");
            }
        }



    }

}

