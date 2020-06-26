using ImpressDev.DAL;
using ImpressDev.Models;
using MvcSiteMapProvider;
using System.Collections.Generic;

namespace ImpressDev.Infrastructure
{
    public class CategoriesDynamicNodeProvider : DynamicNodeProviderBase
    {
        private ImpressDevContext db = new ImpressDevContext();

        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Category category in db.Categories)
            {
                DynamicNode node = new DynamicNode();
                node.Title = category.Name;
                node.Key = "Category_" + category.CategoryId;
                node.RouteValues.Add("categoryName", category.Name);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}