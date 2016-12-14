using ObajuStore.Common;
using ObajuStore.Common.Helpers;
using ObajuStore.Data.Infrastructure;
using ObajuStore.Data.Repositories;
using ObajuStore.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace ObajuStore.Service
{
    public interface IProductService
    {
        Product Add(Product product);

        void Update(Product product);

        Product Delete(long id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> GetLastest(int top);

        IEnumerable<Product> GetTopSales(int top);

        IEnumerable<Product> GetTopView(int top);

        IEnumerable<Product> GetAll(string keyword);

        IEnumerable<string> GetProductsByName(string name);

        IEnumerable<Product> GetRelatedProducts(long id, int top);

        IEnumerable<Product> GetAllPaging(int page, int brandid, string sort, int pageSize, out int totalRow);

        Product GetByID(long id);

        List<Product> GetAllPagingAjax(string keyword);

        IEnumerable<Product> GetByCategoryIDPaging(int categoryId, int brandid, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetByKeywordPaging(string keyword, int brandid, int page, int pageSize, string sort, out int totalRow);

        IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        IEnumerable<Tag> GetTagsByProduct(long id);

        IEnumerable<Product> ProductsByTag(string tagId, string sort, int brandid, int page, int pageSize, out int totalRow);

        void IncreaseView(long id);

        bool SellingProduct(long productId, int quantity);

        Tag GetTag(string tagId);

        IEnumerable<Product> GetHot(int top);

        void SaveChanges();
    }

    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IProductTagRepository _productTagRepository;
        private ITagRepository _tagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IProductTagRepository productTagRepository,
            ITagRepository tagRepository, IUnitOfWork unitOfWork
            )
        {
            _productRepository = productRepository;
            _productTagRepository = productTagRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        public Product Add(Product product)
        {
            product.PromotionPrice = 0;
            var _product = _productRepository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);

                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }

                    ProductTag productTag = new ProductTag();

                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
            return _product;
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
            if (!string.IsNullOrEmpty(product.Tags))
            {
                string[] tags = product.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);

                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.ProductTag;
                        _tagRepository.Add(tag);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag();
                    productTag.ProductID = product.ID;
                    productTag.TagID = tagId;
                    _productTagRepository.Add(productTag);
                }
            }
        }

        public Product Delete(long id)
        {
            return _productRepository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll(new string[] { "ProductCategory" });
        }

        public List<Product> GetAllPagingAjax(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return _productRepository.GetAll().ToList();
            else
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Content.Contains(keyword)).ToList();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetAllPaging(int page, int brandid, string sort, int pageSize, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status);
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.Where(x => x.PromotionPrice.HasValue).OrderByDescending(y => y.PromotionPrice);
                    break;

                case "price_asc":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "price_des":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            if (brandid != 0)
            {
                query = _productRepository.GetMulti(x => x.BrandID == brandid);
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public Product GetByID(long id)
        {
            return _productRepository.GetSingleById(id);
        }

        public IEnumerable<Product> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            return _productRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetLastest(int top)
        {
            return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetTopSales(int top)
        {
            return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetByCategoryIDPaging(int categoryId, int brandid, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.Where(x => x.PromotionPrice.HasValue).OrderByDescending(y => y.PromotionPrice);
                    break;

                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "price_asc":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "price_des":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            if (brandid != 0)
            {
                query = _productRepository.GetMulti(x => x.BrandID == brandid && x.CategoryID == categoryId);
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<string> GetProductsByName(string name)
        {
            return _productRepository.GetMulti(x => x.Status && x.Name.Contains(name)).Select(n => n.Name);
        }

        public IEnumerable<Product> GetByKeywordPaging(string keyword, int brandid, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _productRepository.GetMulti(x => x.Status && x.Name.Contains(keyword));
            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    query = query.Where(x => x.PromotionPrice.HasValue).OrderByDescending(y => y.PromotionPrice);
                    break;

                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "price_asc":
                    query = query.OrderBy(x => x.Price);
                    break;

                case "price_des":
                    query = query.OrderByDescending(x => x.Price);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            if (brandid != 0)
            {
                query = _productRepository.GetMulti(x => x.BrandID == brandid);
            }
            totalRow = query.Count();
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Product> GetRelatedProducts(long id, int top)
        {
            var product = _productRepository.GetSingleById(id);
            return _productRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Tag> GetTagsByProduct(long id)
        {
            return _productTagRepository.GetMulti(x => x.ProductID == id, new string[] { "Tag" }).Select(y => y.Tag);
        }

        public IEnumerable<Product> ProductsByTag(string tagId, string sort, int brandid, int page, int pageSize, out int totalRow)
        {
            var model = _productRepository.GetProductsByTag(tagId, brandid);
            switch (sort)
            {
                case "popular":
                    model = model.OrderByDescending(x => x.ViewCount);
                    break;

                case "discount":
                    model = model.Where(x => x.PromotionPrice.HasValue).OrderByDescending(y => y.PromotionPrice);
                    break;

                case "price":
                    model = model.OrderBy(x => x.Price);
                    break;

                case "price_asc":
                    model = model.OrderBy(x => x.Price);
                    break;

                case "price_des":
                    model = model.OrderByDescending(x => x.Price);
                    break;

                default:
                    model = model.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = model.Count();
            return model.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void IncreaseView(long id)
        {
            var product = _productRepository.GetSingleById(id);
            if (product.ViewCount.HasValue)
                product.ViewCount += 1;
            else
                product.ViewCount = 1;
            _unitOfWork.Commit();
        }

        public Tag GetTag(string tagId)
        {
            return _tagRepository.GetSingleByCondition(x => x.ID == tagId);
        }

        public IEnumerable<Product> GetTopView(int top)
        {
            return _productRepository.GetMulti(x => x.Status).OrderByDescending(x => x.ViewCount).Take(top);
        }

        public IEnumerable<Product> GetHot(int top)
        {
            return _productRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public bool SellingProduct(long productId, int quantity)
        {
            var product = _productRepository.GetSingleById(productId);
            if (product.Quantity < quantity)
                return false;
            product.Quantity -= quantity;
            return true;
        }
    }
}