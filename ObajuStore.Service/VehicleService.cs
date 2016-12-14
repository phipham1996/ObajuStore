using System;
using System.Collections.Generic;
using ObajuStore.Data.Infrastructure;
using ObajuStore.Data.Repositories;
using ObajuStore.Model.Models;

namespace ObajuStore.Service
{
    public interface IVehicleService
    {
        Vehicle Add(Vehicle vehicle);

        void Update(Vehicle vehicle);

        Vehicle GetById(int id);

        IEnumerable<Vehicle> GetAll(string keyword);

        Vehicle Delete(int id);

        void SaveChanges();

    }

    public class VehicleService : IVehicleService
    {
        private IVehicleRepository _vehicleRepository;
        private IUnitOfWork _unitOfWork;

        public VehicleService(IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public Vehicle Add(Vehicle vehicle)
        {
            vehicle.CreatedDate = DateTime.Now;
            return _vehicleRepository.Add(vehicle);
        }

        public void Update(Vehicle vehicle)
        {
            vehicle.UpdatedDate = DateTime.Now;
            _vehicleRepository.Update(vehicle);
        }

        public Vehicle Delete(int id)
        {
            return _vehicleRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Vehicle GetById(int id)
        {
            return _vehicleRepository.GetSingleById(id);
        }

        public IEnumerable<Vehicle> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _vehicleRepository.GetMulti(x => x.ModelID.Contains(keyword) || x.Model.Contains(keyword) || x.DriverName.Contains(keyword) || x.Name.Contains(keyword));
            else
                return _vehicleRepository.GetMulti(x => x.Status);
        }
    }
}