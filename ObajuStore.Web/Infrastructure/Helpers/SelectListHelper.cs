using ObajuStore.Model.Models;
using ObajuStore.Service;
using ObajuStore.Web.Infrastructure.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ObajuStore.Web.Infrastructure.Helpers
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> GetCategoryList(IProductCategoryService categoryService)
        {
            var categories = GetAllCategories(categoryService.GetByDisplayOrder());
            var result = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in categories)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            return result;
        }

        public static IEnumerable<SelectListItem> GetAllCategories(IEnumerable<ProductCategory> categories)
        {
            return categories.Select(c => new SelectListItem
            {
                Text = c.GetFormattedBreadCrumb(categories.ToList()),
                Value = c.ID.ToString()
            });
        }

    }
}