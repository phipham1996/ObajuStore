using System;
using System.Collections.Generic;
using ObajuStore.Data.Infrastructure;
using ObajuStore.Data.Repositories;
using ObajuStore.Model.Models;
using System.Linq;

namespace ObajuStore.Service
{
    public interface IBrandService
    {
        Brand Add(Brand brand);

        void Update(Brand brand);

        void Delete(int id);

        void SaveChanges();

        Brand GetByID(int id);

        IEnumerable<Brand> GetAll(string keyword);

        IEnumerable<Brand> GetAllPaging(string q, int page, int pageSize, out int totalow);

        IEnumerable<Brand> GetActivedBrand(string keyword);
    }

    public class BrandService : IBrandService
    {
        private IBrandRepository _brandRepository;
        private IUnitOfWork _unitOfWork;

        public BrandService(IBrandRepository brandRepository,
            IUnitOfWork unitOfWork)
        {
            this._brandRepository = brandRepository;
            this._unitOfWork = unitOfWork;
        }

        public Brand Add(Brand brand)
        {
            brand.IsDeleted = false;
            return _brandRepository.Add(brand);
        }

        public void Delete(int id)
        {
            var brand = _brandRepository.GetSingleById(id);
            brand.IsDeleted = true;
            _brandRepository.Update(brand);
            SaveChanges();
        }

        public IEnumerable<Brand> GetActivedBrand(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _brandRepository.GetMulti(x => x.Status && x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return _brandRepository.GetMulti(x => x.Status);
            }
        }

        public IEnumerable<Brand> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _brandRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword) && x.IsDeleted == false);
            }
            else
            {
                return _brandRepository.GetMulti(x => x.Status && x.IsDeleted == false);
            }
        }

        public IEnumerable<Brand> GetAllPaging(string q, int page, int pageSize, out int totalow)
        {
            var query = _brandRepository.GetMulti(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(q))
                query = query.Where(x => x.Name.ToLower().Contains(q.ToLower())
                 || x.Country.ToLower().Contains(q.ToLower())
                 || x.Website.ToLower().Contains(q.ToLower()));

            totalow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public Brand GetByID(int id)
        {
            return _brandRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Brand brand)
        {
            _brandRepository.Update(brand);
        }
    }
}