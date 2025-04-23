using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UF3_test.model
{
    [Serializable]
    public class Book
    {
        public int _id { get; set; }
        public String title { get; set; }
        public String isbn { get; set; }
        public int pageCount { get; set; }
        public PublishedDate publishedDate { get; set; }
        public String thumbnailUrl { get; set; }
        public String shortDescription { get; set; }
        public String longDescription { get; set; }
        public String status { get; set; }
        public List<String> authors { get; set; }
        public List<String> categories { get; set; }

        public override string ToString()
        {
            return
                "Book{" +
                "_id = '" + _id + '\'' +
                ",title = '" + title + '\'' +
                ",isbn = '" + isbn + '\'' +
                ",pageCount = '" + pageCount + '\'' +
                ",publishedDate = '" + publishedDate + '\'' +
                ",thumbnailUrl = '" + thumbnailUrl + '\'' +
                ",shortDescription = '" + shortDescription + '\'' +
                ",longDescription = '" + longDescription + '\'' +
                ",status = '" + status + '\'' +
                ",authors = '" + authors + '\'' +
                ",categories = '" + categories + '\'' +
                "}";
        }
    }
}
