using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Driver;
using Newtonsoft.Json;
using UF3_test.connections;
using UF3_test.model;

namespace cat.itb.NF3EA3_VillodresAdrian.cruds
{
    public class BooksCRUD
    {
        // Mètode per carregar una col·lecció de llibres
        public void LoadBooksCollection()
        {
            FileInfo file = new FileInfo("../../../files/books.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();

            List<Book> books = JsonConvert.DeserializeObject<List<Book>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("books");
            var collection = database.GetCollection<Book>("books");

            if (books != null)
            {
                collection.InsertMany(books);
                foreach (var book in books)
                {
                    Console.WriteLine(book.title);
                }
            }
        }

        // Mètode per mostrar llibres filtrant per pàgines i categoria
        public void ShowBooksByPagesAndCategory(int minPages, int maxPages, string category)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<Book>("books");

            var books = collection.AsQueryable()
                .Where(b => b.pageCount >= minPages
                         && b.pageCount <= maxPages
                         && b.categories != null && b.categories.Contains(category))
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"Títol: {book.title}");
                Console.WriteLine($"Autors: {string.Join(", ", book.authors ?? new List<string>())}");
                Console.WriteLine($"Num. Pàgines: {book.pageCount}");
                Console.WriteLine("------------------------------------");
            }
        }

        // Mètode per mostrar llibres filtrant per array d'autors
        public void SelectByAuthors(string[] authors)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<Book>("books");

            var books = collection.AsQueryable()
                .Where(b => b.authors != null && authors.All(a => b.authors.Contains(a)))
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"Títol: {book.title}");
                Console.WriteLine($"Autors: {string.Join(", ", book.authors ?? new List<string>())}");
                Console.WriteLine("------------------------------------");
            }
        }

        // Mètode per mostrar títols i estats dels llibres
        public void SelectBooksTitleAndStatus()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<Book>("books");

            var books = collection.AsQueryable()
                .Select(b => new { b.title, b.status })
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"Títol: {book.title}");
                Console.WriteLine($"Estat: {book.status}");
                Console.WriteLine("------------------------------------");
            }
        }

        // Mètode per seleccionar llibres ordenats per número de pàgines (descendent)
        public void SelectBooksOrderByPages()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<Book>("books");

            var books = collection.AsQueryable()
                .OrderByDescending(b => b.pageCount)
                .ToList()
                .Select(b => new
                {
                    b.title,
                    Categories = string.Join(", ", b.categories ?? new List<string>())
                })
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"Títol: {book.title}");
                Console.WriteLine($"Categories: {book.Categories}");
                Console.WriteLine("------------------------------------");
            }
        }


        // Mètode per seleccionar llibres per autor concret
        public void SelectBooksByAuthor(string author)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<Book>("books");

            var books = collection.AsQueryable()
                .Where(b => b.authors != null && b.authors.Contains(author))
                .ToList()
                .Select(b => new
                {
                    b.title,
                    Authors = string.Join(", ", b.authors ?? new List<string>())
                })
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"Títol: {book.title}");
                Console.WriteLine($"Autors: {book.Authors}");
                Console.WriteLine("------------------------------------");
            }
        }


        // Mètode per seleccionar llibres per categoria excloent un autor
        public void SelectBooksByCategoryExcludingAuthor(string category, string excludedAuthor)
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<Book>("books");

            var books = collection.AsQueryable()
                .Where(b => b.categories != null && b.categories.Contains(category)
                         && (b.authors == null || !b.authors.Contains(excludedAuthor)))
                .OrderBy(b => b.title)
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"Títol: {book.title}");
                Console.WriteLine($"Categories: {string.Join(", ", book.categories ?? new List<string>())}");
                Console.WriteLine("------------------------------------");
            }
        }
    }
}
