using AutoMapper;
using ObajuStore.Data;
using ObajuStore.Model.Models;
using ObajuStore.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace ObajuStore.Web.Infrastructure.Extensions
{
    public class MenuHelper
    {
        private ObajuStoreDbContext db = null;

        public MenuHelper()
        {
            db = new ObajuStoreDbContext();
        }

        public IEnumerable<ProductCategoryViewModel> GetChildByID(int id)
        {
            var childs = db.ProductCategories.Where(x => x.ParentID == id).AsEnumerable();
            var childsModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(childs);
            return childsModel;
        }

        public IEnumerable<ProductCategoryViewModel> GetShowInFooter(int id)
        {
            var childs = db.ProductCategories.Where(x => x.ParentID == id && x.HomeFlag == true).OrderByDescending(x => x.CreatedDate).Take(3).AsEnumerable();
            var childsModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(childs);
            return childsModel;
        }
    }
}