using ImpressDev.DAL;
using ImpressDev.Models;
using MvcSiteMapProvider;
using System.Collections.Generic;

namespace ImpressDev.Infrastructure
{
    public class BooksDynamicNodeProvider : DynamicNodeProviderBase
    {
        private ImpressDevContext db = new ImpressDevContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Book book in db.Books)
            {
                DynamicNode node = new DynamicNode();
                node.Title = book.Title;
                node.Key = "Book_" + book.BookId;
                node.ParentKey = "Category_" + book.CategoryId;
                node.RouteValues.Add("bookId", book.BookId);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}