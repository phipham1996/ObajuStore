using ObajuStore.Common.Exceptions;
using ObajuStore.Data.Infrastructure;
using ObajuStore.Data.Repositories;
using ObajuStore.Model.Models;
using System.Collections.Generic;

namespace ObajuStore.Service
{
    public interface IApplicationRoleService
    {
        IEnumerable<ApplicationRole> GetAll(string filter = null);

        ApplicationRole Add(ApplicationRole appRole);

        void Update(ApplicationRole AppRole);

        void Delete(string id);

        ApplicationRole FindById(string id);

        void Save();
    }

    public class ApplicationRoleService : IApplicationRoleService
    {
        private IApplicationRoleRepository _appRole;
        private IUnitOfWork _unitOfWork;

        public ApplicationRoleService(IUnitOfWork unitOfWork,
            IApplicationRoleRepository appRole)
        {
            this._appRole = appRole;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationRole Add(ApplicationRole appRole)
        {
            appRole.IsDeleted = false;
            if (_appRole.CheckContains(x => x.Description == appRole.Description))
                throw new NameDuplicatedException("Tên không được trùng");
            return _appRole.Add(appRole);
        }

        public void Delete(string id)
        {
            var role = _appRole.GetSingleById(id);
            role.IsDeleted = true;
            Save();
        }

        public ApplicationRole FindById(string id)
        {
            return _appRole.GetSingleById(id);
        }

        public IEnumerable<ApplicationRole> GetAll(string filter = null)
        {
            if (!string.IsNullOrEmpty(filter))
                return _appRole.GetMulti(x => x.Name.Contains(filter) || x.Description.Contains(filter) && x.IsDeleted == false);
            return _appRole.GetMulti(x => x.IsDeleted == false);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationRole AppRole)
        {
            if (_appRole.CheckContains(x => x.Description == AppRole.Description && x.Id != AppRole.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _appRole.Update(AppRole);
        }
    }
}