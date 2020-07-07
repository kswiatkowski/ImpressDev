using ImpressDev.Models;
using System.Collections.Generic;

namespace ImpressDev.ViewModels
{
    public class EditBookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public bool? Confirm { get; set; }
    }
}