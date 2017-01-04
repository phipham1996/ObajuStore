using ObajuStore.Data.Infrastructure;
using ObajuStore.Data.Repositories;
using ObajuStore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ObajuStore.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory ProductCategory);

        void Update(ProductCategory ProductCategory);

        ProductCategory Delete(long id);

        IEnumerable<ProductCategory> GetAll();

        IEnumerable<ProductCategory> GetParentProductCategory();

        IEnumerable<ProductCategory> GetByDisplayOrder();

        IEnumerable<ProductCategory> GetAllPaging(string q, int page, int pageSize, out int totalRow, bool isDisplayOrder = false);

        IEnumerable<ProductCategory> GetAll(string keyword);

        IEnumerable<ProductCategory> GetAllByParentID(int parentID);

        ProductCategory GetByID(int id);

        void SaveChanges();
    }

    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _productCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._productCategoryRepository = productCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public ProductCategory Add(ProductCategory productCategory)
        {
            productCategory.CreatedDate = DateTime.Now;
            return _productCategoryRepository.Add(productCategory);
        }

        public void Update(ProductCategory productCategory)
        {
            productCategory.UpdatedDate = DateTime.Now;
            _productCategoryRepository.Update(productCategory);
        }

        public ProductCategory Delete(long id)
        {
            return _productCategoryRepository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _productCategoryRepository.GetAll();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<ProductCategory> GetAllByParentID(int parentID)
        {
            return _productCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentID);
        }

        public ProductCategory GetByID(int id)
        {
            return _productCategoryRepository.GetSingleById(id);
        }


        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productCategoryRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productCategoryRepository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAllPaging(string q, int page, int pageSize, out int totalRow, bool isDisplayOrder = false)
        {
            var query = _productCategoryRepository.GetAll();
            if (isDisplayOrder)
            {
                query = _productCategoryRepository.GetAll();
                totalRow = query.Count();
                return query.OrderBy(x => x.DisplayOrder).Skip((page - 1) * pageSize).Take(pageSize);
            }
            else
            {
                if (!string.IsNullOrEmpty(q))
                    query = query.Where(x => x.Name.ToLower().Contains(q.ToLower()) || x.Description.ToLower().Contains(q.ToLower())).OrderByDescending(x => x.CreatedDate);
                totalRow = query.Count();
                return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
            }
        }

        public IEnumerable<ProductCategory> GetParentProductCategory()
        {
            var category = _productCategoryRepository.GetMulti(x => x.ParentID == null || x.ParentID < 1);
            return category;
        }

        public IEnumerable<ProductCategory> GetByDisplayOrder()
        {
            var query = _productCategoryRepository.GetMulti(x => x.Status).OrderBy(x => x.DisplayOrder);
            return query;
        }
    }
}