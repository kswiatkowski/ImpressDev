using ImpressDev.Models;
using System.Collections.Generic;

namespace ImpressDev.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Book> News { get; set; }
        public IEnumerable<Book> Bestsellers { get; set; }
    }
}