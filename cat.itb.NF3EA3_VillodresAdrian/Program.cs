using cat.itb.NF3EA1_VillodresAdrian.Model;
using cat.itb.NF3EA3_VillodresAdrian.cruds;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UF3_test.connections;
using UF3_test.model;

namespace cat.itb.NF3EA1_VillodresAdrian.cruds
{
    class Program
    {

        static void Main(string[] args)
        {
            {
                BooksCRUD books = new BooksCRUD();
                ProductsCRUD products = new ProductsCRUD();
                bool exit = false;

                while (!exit)
                {
                    Console.WriteLine("Choose an option:");
                    Console.WriteLine("EXERCICI 1: Importació de coleccions");
                    Console.WriteLine("     1. Importar Coleccions");
                    Console.WriteLine("");
                    Console.WriteLine("EXERCICI 2: Eliminació de coleccions");
                    Console.WriteLine("     2: Eliminar una colecció.");
                    Console.WriteLine("");
                    Console.WriteLine("EXERCICI 3: QUERIES AMB LINQ");
                    Console.WriteLine("     3: Mostra el nom i el preu del producte amb el preu més alt.");
                    Console.WriteLine("     4: Mostra la suma de tots els stocks de la col·lecció de productes.");
                    Console.WriteLine("     5: Mostra el title, authors i pageCount dels llibres que tinguin entre 300 i 350 pàgines (ambdues incloses) i siguin de categoria \"Java\".");
                    Console.WriteLine("     6: Mostra el title, authors dels llibres on han participat almenys els autors: \"Charlie Collins\" i \"Robi Sen\". Al mètode li passarem un Array de Strings (String[]) amb els noms dels autors.");
                    Console.WriteLine("     7: Mostra únicament els camp title i status de tots els llibres.");
                    Console.WriteLine("     8: Mostra el títol de tots els llibres i les categories a que pertanyen i els ordenes de forma descendent per número de pàgines.");
                    Console.WriteLine("     9: Mostra el title i authors dels llibres de \"Danno Ferrin\". Al mètode li passarem un String amb el nom de l’autor.");
                    Console.WriteLine("     10: Mostra els llibres que tinguin dins el camp categories la categoria \"Java\" però descartem els que surti \"Vikram Goyal\" com author. Ordena de forma ascendent per title.");
                    Console.WriteLine("");
                    Console.WriteLine("0. Exit");
                    Console.Write("Option: ");


                    switch (Console.ReadLine())
                    {
                        case "1":
                            Console.WriteLine("");
                            books.LoadBooksCollection();
                            products.LoadProductsCollection();
                            Console.WriteLine("Coleccions importades correctament");
                            Console.WriteLine("");
                            break;
                        case "2":
                            Console.WriteLine("");
                            DropCollection("itb", "books");
                            Console.WriteLine("");
                            break;
                        case "3":
                            Console.WriteLine("");
                            products.SelectByExpesivePrice();
                            Console.WriteLine("");
                            break;
                        case "4":
                            Console.WriteLine("");
                            products.CountSumProducts();
                            Console.WriteLine("");
                            break;
                        case "5":
                            Console.WriteLine("");
                            books.ShowBooksByPagesAndCategory(300, 350, "Java");
                            Console.WriteLine("");
                            break;
                        case "6":
                            Console.WriteLine("");
                            books.SelectByAuthors(new string[] { "Charlie Collins", "Robi Sen" });
                            Console.WriteLine("");
                            break;
                        case "7":
                            Console.WriteLine("");
                            books.SelectBooksTitleAndStatus();
                            Console.WriteLine("");
                            break;
                        case "8":
                            Console.WriteLine("");
                            books.SelectBooksOrderByPages();
                            Console.WriteLine("");
                            break;
                        case "9":
                            Console.WriteLine("");
                            books.SelectBooksByAuthor("Danno Ferrin");
                            Console.WriteLine("");
                            break;
                        case "10":
                            Console.WriteLine("");
                            books.SelectBooksByCategoryExcludingAuthor("Java" , "Vikram Goyal");
                            Console.WriteLine("");
                            break;
                        case "0":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("");
                            Console.WriteLine("Opció no válida, intenta de nou.");
                            Console.WriteLine("");
                            break;
                    }
                }

            }
        }
        public static void DropCollection(string databaseName, string collectionName)
        {
            var db = MongoLocalConnection.GetDatabase(databaseName);
            var collection = db.GetCollection<BsonDocument>(collectionName);

            var countBeforeDelete = collection.CountDocuments(new BsonDocument());
            Console.WriteLine($"Número de documents a la col·lecció '{collectionName}': {countBeforeDelete}");

            db.DropCollection(collectionName);
            Console.WriteLine($"Col·lecció '{collectionName}' eliminada.");

            var collections = db.ListCollectionNames().ToList();
            Console.WriteLine("Col·leccions restants a la base de dades:");
            foreach (var collectionNameRemaining in collections)
            {
                Console.WriteLine(collectionNameRemaining);
            }
        }
    }
}