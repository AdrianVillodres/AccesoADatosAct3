using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Nodes;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using UF3_test.connections;
using UF3_test.model;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace UF3_test
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            //EA1
            //GetAllDBs();      
            //GetCollections();
            //SelectAllStudents();
            //InsertOneStudent();
            //SelectOneStudent();
            //SelectStudentFields();            
            //LoadPeopleCollection();
            //LoadBooksCollection();
            //LoadProductsCollection();


            //EA2
            //LoadPeopleCollection();
            //SelectPeopleByFriend();
            //SelectPeopleByAge();
            //UpdateOnePerson();
            //UpdateManyPeople();
            //DeleteOnePerson();
            //DeleteManyPeople();
            //UpdatePeopleArrayPopLast();
            //UpdatePeopleNewField();


            //Actualitzat

            //DropCollection("itb", "products");
            //DropCollection("itb", "people");
            // DropCollection("itb", "books");

            //carreguem documents sense _id, MongoBD afegeix el _id
            //loadProductsDocsCollection();
            //Quan volem obtenir una col·lecció d'objecte Product falla perquè no troba l'atribut _id
            //SelectAllProducts(); 
            //Hem d'utilitzar un DTO amb _id  [BsonRepresentation(BsonType.ObjectId)]
            //SelectAllProducts2();

            //carreguem documents amb _id que afegim nosaltres al fitxer JSON
            //loadProducts2DocsCollection();
            //Obtenim els objectes amb _id nostre
            //SelectAllProducts2();           

            //Ara carreguem directament els objectes sense el _id nostre
            //loadProductsObjectCollection();
            //I podem obtenir els objectes amb el _id creat al MongoDB
            //SelectAllProducts2();

            //loadBooks2DocsCollection();
            //SelectOneBook2();
            //SelectOneBook();

            //loadBooks2ObjectCollection();
            //SelectOneBook2();
            //SelectOneBook();


            //EA3: LINQ Queries
            //SelectAllProducts2();
            //SelectOneProductA();
            //SelectOneProductB();
            //SelectOneBook();
            //SelectFirstAuthorFromBookA();
            //SelectFirstAuthorFromBookB();
            //SelectBooksByPageCount();
            //SelectBiggerBook();

            // LoadPeopleObjectCollection();
            // SelectOnePersonObject();


            //Si carreguem documents amb $Date i PublishedDate
            //loadBooksDocsCollection();
            //Al passar-ho a objectes falla
            //SelectOneBook();
            

            //Però si carrego el fitxer amb $Date i i la classe PublishedDate2 com objectes
            //loadBooksObjectCollection();
            //Després si que puc recuperar els objectes
            //SelectOneBook();
            //SelectFirstAuthorFromBook();


        }

        private static void GetAllDBs()
        {


            var dbClient = MongoLocalConnection.GetMongoClient();

            var dbList = dbClient.ListDatabases().ToList();
            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }
            Console.ReadKey();
            Console.Clear();

        }

        private static void GetCollections()
        {

            var database = MongoLocalConnection.GetDatabase("sample_training");

            var colList = database.ListCollections().ToList();
            Console.WriteLine("The list of collection on this database is: ");
            foreach (var col in colList)
            {
                Console.WriteLine(col);
            }
            Console.ReadKey();
            Console.Clear();
        }

        private static void SelectAllStudents()
        {


            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var studentDocuments = collection.Find(new BsonDocument()).ToList();

            foreach (var student in studentDocuments)
            {
                Console.WriteLine(student.ToString());
            }
            Console.ReadKey();
            Console.Clear();

        }

        private static void InsertOneStudent()
        {

            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var document = new BsonDocument
            {
                { "student_id", 9999923 },
                { "scores", new BsonArray
                {
                        new BsonDocument{ {"type", "exam"}, {"score", 88.12334193287023 } },
                        new BsonDocument{ {"type", "quiz"}, {"score", 74.92381029342834 } },
                        new BsonDocument{ {"type", "homework"}, {"score", 89.97929384290324 } },
                        new BsonDocument{ {"type", "homework"}, {"score", 82.12931030513218 } }
                    }
                },
                { "class_id", 480}
            };


            collection.InsertOne(document);
            Console.WriteLine(document.GetElement("student_id").ToString() + "   inserted");
            Console.ReadKey();
            Console.Clear();

        }

        private static void SelectOneStudent()
        {


            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", 9999923);
            var studentDocument = collection.Find(filter).FirstOrDefault();
            Console.WriteLine(studentDocument.ToString());
            Console.ReadKey();
            Console.Clear();

        }


        private static void SelectStudentFields()
        {

            var database = MongoLocalConnection.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            var filter = Builders<BsonDocument>.Filter.Eq("student_id", 9999923);
            var studentDocument = collection.Find(filter).FirstOrDefault();
            var id = studentDocument.GetElement("student_id");
            var scores = studentDocument.GetElement("scores");

            Console.WriteLine(id.ToString());
            Console.WriteLine(scores.ToString());
            Console.ReadKey();
            Console.Clear();


        }

        private static void LoadPeopleCollection()
        {
            FileInfo file = new FileInfo("../../../files/people.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Person> people = JsonConvert.DeserializeObject<List<Person>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("people");
            var collection = database.GetCollection<BsonDocument>("people");

            if (people != null)
                foreach (var person in people)
                {
                    Console.WriteLine(person.Name);
                    string json = JsonConvert.SerializeObject(person);
                    var document = new BsonDocument();
                    //document.Add(BsonDocument.Parse(json));
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            Console.ReadKey();
            Console.Clear();

        }

        private static void LoadBooksCollection()
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
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                    string json = JsonConvert.SerializeObject(book);
                    var document = new BsonDocument();
                    //document.Add(BsonDocument.Parse(json));
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            Console.ReadKey();
            Console.Clear();

        }

        private static void LoadProductsCollection()
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
                    Console.WriteLine(product.Name);
                    string json = JsonConvert.SerializeObject(product);
                    var document = new BsonDocument();
                    //document.Add(BsonDocument.Parse(json));
                    document.AddRange(BsonDocument.Parse(json)); 
                    collection.InsertOne(document);
                }
            }
            Console.ReadKey();
            Console.Clear();

        }

        private static void SelectPeopleByFriend()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

          
            var friendFilter1 = Builders<BsonDocument>.Filter.Eq("friends.name", "Serenity Watson");

            var people = collection.Find(friendFilter1).ToList();
            

            foreach (var person in people)
            {
                Console.WriteLine(person.ToString());
            }

            Console.WriteLine();

            var friendFilter2 = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>(
                "friends", new BsonDocument { { "name", "Rachel Hancock" } });

            var cursor = collection.Find(friendFilter2).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document.ToString());
                Console.WriteLine();

            }
            Console.ReadKey();
            Console.Clear();
        }
        private static void SelectPeopleByAge()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

            var ageFilter = Builders<BsonDocument>.Filter.Gt("age", 38);
            //var cursor = collection.Find(ageFilter).ToCursor();

            var sort = Builders<BsonDocument>.Sort.Descending("age");
            var cursor = collection.Find(ageFilter).Sort(sort).ToCursor();

            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document.ToString());
                Console.WriteLine();
            }
            Console.ReadKey();
            Console.Clear();
        }

        private static void UpdateOnePerson()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "Sophie Gibbs");
            var update = Builders<BsonDocument>.Update.Set("isActive", false);

            var personDoc1 = collection.Find(filter).First();

            var name1 = personDoc1.GetElement("name");
            var isActive1 = personDoc1.GetElement("isActive");

            Console.WriteLine(name1.ToString() + "  " + isActive1.ToString());

            collection.UpdateOne(filter, update);

            var personDoc2 = collection.Find(filter).First();

            var name2 = personDoc2.GetElement("name");
            var isActive2 = personDoc2.GetElement("isActive");

            Console.WriteLine(name2.ToString() + "  " + isActive2.ToString());

            Console.ReadKey();
            Console.Clear();

        }

        private static void UpdateManyPeople()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

            var filter = Builders<BsonDocument>.Filter.Eq("randomArrayItem", "teacher") & Builders<BsonDocument>
                .Filter.Eq("gender", "male");
            var update = Builders<BsonDocument>.Update.Set("company", "Treson");

            var docsUpdated = collection.UpdateMany(filter, update);
            Console.WriteLine("Docs modificats: " + docsUpdated.ModifiedCount);
            Console.WriteLine();

            var cursor = collection.Find(filter).ToCursor();

            foreach (var doc in cursor.ToEnumerable())
            {
                Console.WriteLine(doc.GetValue("company"));
                Console.WriteLine();

            }

            Console.ReadKey();
            Console.Clear();
        }

        private static void DeleteOnePerson()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

            var deleteFilter = Builders<BsonDocument>.Filter.Eq("name", "Eva Watson");

            var docsDeleted = collection.DeleteOne(deleteFilter);
            Console.WriteLine("Docs eliminats: " + docsDeleted.DeletedCount);

            Console.ReadKey();
            Console.Clear();

        }

        private static void DeleteManyPeople()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

            var deleteFilter = Builders<BsonDocument>.Filter.Lt("age", 23);

            var docsDeleted = collection.DeleteMany(deleteFilter);
            Console.WriteLine("Docs eliminats: " + docsDeleted.DeletedCount);

            Console.ReadKey();
            Console.Clear();
        }
        private static void UpdatePeopleArrayPopLast()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "Sophie Gibbs");
            var update = Builders<BsonDocument>.Update.PopLast("tags");

            var personDoc1 = collection.Find(filter).First();
            var tags1 = personDoc1.GetElement("tags");
            Console.WriteLine(tags1.ToString());

            collection.UpdateOne(filter, update);

            var personDoc2 = collection.Find(filter).First();
            var tags2 = personDoc2.GetElement("tags");
            Console.WriteLine(tags2.ToString());

            Console.ReadKey();
            Console.Clear();

        }

        private static void UpdatePeopleNewField()
        {
            var database = MongoLocalConnection.GetDatabase("itb");
            var collection = database.GetCollection<BsonDocument>("people");

            var filter = Builders<BsonDocument>.Filter.Eq("name", "Madelyn Murphy");
            var update = Builders<BsonDocument>.Update.Set("phone_aux", "666-470-34822");

            collection.UpdateOne(filter, update);

            var personDoc = collection.Find(filter).First();
            Console.WriteLine(personDoc.ToString());

            Console.ReadKey();
            Console.Clear();

        }

        private static void loadProductsDocsCollection()
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
                    Console.WriteLine(product?.Name);
                    string json = JsonConvert.SerializeObject(product);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            }

        }

        private static void loadProducts2DocsCollection()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("products");
            var collection = database.GetCollection<BsonDocument>("products");

            FileInfo file = new FileInfo("../../../files/products2.json");

            using (StreamReader sr = file.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Product2 product = JsonConvert.DeserializeObject<Product2>(line);
                    Console.WriteLine(product?.Name);
                    string json = JsonConvert.SerializeObject(product);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
            }

        }

        private static void loadProductsObjectCollection()
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
                    Console.WriteLine(product.Name);
                    //string json = JsonConvert.SerializeObject(product);
                    //var document = new BsonDocument();
                    //document.Add(BsonDocument.Parse(json));
                    collection.InsertOne(product);
                }
            }

        }

        private static void loadBooksDocsCollection()
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
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                    string json = JsonConvert.SerializeObject(book);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
        }


        private static void loadBooksObjectCollection()
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
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                    //string json = JsonConvert.SerializeObject(book);
                    //var document = new BsonDocument();
                    //document.Add(BsonDocument.Parse(json));
                    collection.InsertOne(book);
                }
        }

        private static void loadBooks2DocsCollection()
        {
            FileInfo file = new FileInfo("../../../files/books2.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Book2> books = JsonConvert.DeserializeObject<List<Book2>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");

            database.DropCollection("books");

            var collection = database.GetCollection<BsonDocument>("books");

            if (books != null)
                foreach (var book in books)
                {
                    Console.WriteLine(book.Title);
                    string json = JsonConvert.SerializeObject(book);
                    var document = new BsonDocument();
                    document.AddRange(BsonDocument.Parse(json));
                    collection.InsertOne(document);
                }
        }


        private static void loadBooks2ObjectCollection()
        {
            FileInfo file = new FileInfo("../../../files/books2.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Book2> books = JsonConvert.DeserializeObject<List<Book2>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");

            database.DropCollection("books");

            var collection = database.GetCollection<Book2>("books");

            if (books != null)
                foreach (var book2 in books)
                {
                    Console.WriteLine(book2.Title);
                    // string json = JsonConvert.SerializeObject(book);
                    // var document = new BsonDocument();
                    // document.Add(BsonDocument.Parse(json));
                    collection.InsertOne(book2);
                }
        }

      
        private static void SelectAllProducts()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var productsCollection = database.GetCollection<Product>("products");

            var numProductes =
                productsCollection.AsQueryable<Product>()
                    .Count();

            Console.WriteLine("Número de productes:{0} ", numProductes);
            Console.WriteLine();


            var productList = productsCollection.AsQueryable<Product>().ToList();

            foreach (var product in productList)
            {
               Console.WriteLine("Nom :{0} " + "Preu :{1} ", product.Name, product.Price);
            }

            Console.ReadKey();
            Console.Clear();
        }

     
       
        private static void SelectAllProducts2()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var productsCollection = database.GetCollection<Product2>("products");

            var numProductes =
                productsCollection.AsQueryable<Product2>()
                    .Count();

            Console.WriteLine("Número de productes:{0} ", numProductes);
            Console.WriteLine();


            var productList = productsCollection.AsQueryable<Product2>().ToList();

            foreach (var product in productList)
            {
                Console.WriteLine("Nom :{0} " + "Id :{1} ", product.Name, product._id);
            }

            Console.ReadKey();
            Console.Clear();
        }

        private static void SelectOneProductA()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var productsCollection = database.GetCollection<Product2>("products");

            var query =
                from p in productsCollection.AsQueryable<Product2>()
                where p.Name == "MacBook"
                select p;

            foreach (var product in query)
            {
                Console.WriteLine("Nom :{0} " + "Preu :{1} ", product.Name, product.Price);
            }
        }

        private static void SelectOneProductB()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var productsCollection = database.GetCollection<Product2>("products");

            var query =
                productsCollection.AsQueryable<Product2>()
                    .Where(p => p.Name == "MacBook");

            foreach (var product in query)
            {
                Console.WriteLine("Nom :{0} ", product.Name);

                foreach (var cat in product.Categories)
                {
                    Console.WriteLine("Categoria :{0} ", cat);
                }
            }
        }


        private static void SelectOneBook()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book>("books");


            var query =
                from b in booksCollection.AsQueryable<Book>()
                where b.Title == "Flexible Rails"
                select b;

            foreach (var book in query)
            {
                Console.WriteLine("Nom :{0} " + "Data publicació :{1} ", book.Title, book.PublishedDate);
            }

        }


        private static void SelectOneBook2()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book2>("books");


            var query =
                from b in booksCollection.AsQueryable<Book2>()
                where b.Title == "Flexible Rails"
                select b;

            foreach (var book in query)
            {
                Console.WriteLine("Nom :{0} " + "Data publicació :{1} ", book.Title, book.PublishedDate);
            }

        }

        private static void SelectFirstAuthorFromBookA()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book2>("books");

            var autor =
                booksCollection.AsQueryable<Book2>()
                    .Where(b => b.Title == "Flexible Rails")
                    .Select(b => b.Authors[0])
                    .First();


            Console.WriteLine(autor);


        }

        private static void SelectFirstAuthorFromBookB()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book2>("books");

            var autor =
                booksCollection.AsQueryable<Book2>()
                    .Where(b => b.Title == "Flexible Rails")
                    .Select(b => b.Authors.First())
                    .First();


            Console.WriteLine(autor);


        }

        private static void SelectFirstAuthorFromBook()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book>("books");

            var autor =
                booksCollection.AsQueryable<Book>()
                    .Where(b => b.Title == "Flexible Rails")
                    .Select(b => b.Authors.First())
                    .First();


            Console.WriteLine(autor);


        }

        private static void SelectBooksByPageCount()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book2>("books");

            var query =
                booksCollection.AsQueryable<Book2>()
                    .Where(b => b.PageCount > 900)
                    .OrderBy(b => b.Title);

            foreach (var book in query)
            {
                Console.WriteLine(book.Title);
            }

        }

        private static void SelectBiggerBook()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var booksCollection = database.GetCollection<Book2>("books");

            var query = booksCollection.AsQueryable<Book2>();

            var maxPages =
                query
                    .Select(b => b.PageCount)
                    .Max();

            var book =
                query
                    .Where(b => b.PageCount == maxPages)
                    .Single();

            Console.WriteLine("El llibre amb més pàgines es diu :{0} " + "Número de pàgines :{1} ", book.Title, book.PageCount);

        }

        private static void LoadPeopleObjectCollection()
        {
            FileInfo file = new FileInfo("../../../files/people.json");
            StreamReader sr = file.OpenText();
            string fileString = sr.ReadToEnd();
            sr.Close();
            List<Person2> people = JsonConvert.DeserializeObject<List<Person2>>(fileString);

            var database = MongoLocalConnection.GetDatabase("itb");
            database.DropCollection("people");

            var collection = database.GetCollection<Person2>("people");

            if (people != null)
                foreach (var person2 in people)
                {
                    Console.WriteLine(person2.Name);
                    //string json = JsonConvert.SerializeObject(person);
                    //var document = new BsonDocument();
                    //document.Add(BsonDocument.Parse(json));
                    collection.InsertOne(person2);
                }
        }
        private static void SelectOnePersonObject()
        {

            var database = MongoLocalConnection.GetDatabase("itb");
            var peopleCollection = database.GetCollection<Person2>("people");

            var query =
                from p in peopleCollection.AsQueryable<Person2>()
                where p.Name == "Brooke Stanley"
                select p;

            foreach (var person in query)
            {
                Console.WriteLine("Nom :{0} " + "Telèfon :{1} ", person.Name, person.Phone);
            }

        }


        private static void DropCollection(String db, String col)
        {

            var database = MongoLocalConnection.GetDatabase(db);

            database.DropCollection(col);

            //Utilitzant un cursor

            var cursor = database.ListCollectionNames();

            foreach (var colName in cursor.ToEnumerable())
            {
                Console.WriteLine(colName.ToString());

            }

            //O utilitzant la llista
            /*
            var cols =  database.ListCollections().ToList();
            foreach (var col in cols)
            {
                Console.WriteLine(col.GetElement("name"));
            }
            */

        }

    }
}