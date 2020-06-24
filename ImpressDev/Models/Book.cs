using System;

namespace ImpressDev.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string SubTitle { get; set; }
        public string Level { get; set; }
        public string PublishingHouse { get; set; }
        public string Cover { get; set; }
        public string Pages { get; set; }
        public string Description { get; set; }
        public string PhotoSource { get; set; }
        public decimal Price { get; set; }

        public bool Bestseller { get; set; }
        public bool Inaccessible { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual Category Category { get; set; }
    }
}