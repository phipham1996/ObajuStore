using System;
using System.Collections.Generic;
using ObajuStore.Data.Infrastructure;
using ObajuStore.Data.Repositories;
using ObajuStore.Model.Models;
using System.Linq;

namespace ObajuStore.Service
{
    public interface IApplicationUserService
    {
        ApplicationUser GetUserById(string userId);

        IEnumerable<string> GetUserIdByGroupId(int id);

        IEnumerable<ApplicationUser> GetUserListPaging(int page, int pageSize, string filter, out int totalRow);

        void SetViewed(string id);
    }

    public class ApplicationUserService : IApplicationUserService
    {
        private IApplicationUserRepository _applicationUserRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository,
            IUnitOfWork unitOfWork)
        {
            _applicationUserRepository = applicationUserRepository;
            _unitOfWork = unitOfWork;
        }

        public ApplicationUser GetUserById(string userId)
        {
            try
            {
                return _applicationUserRepository.GetSingleById(userId);
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<string> GetUserIdByGroupId(int id)
        {
            return _applicationUserRepository.GetUserIdByGroupId(id);
        }

        public IEnumerable<ApplicationUser> GetUserListPaging(int page, int pageSize, string filter, out int totalRow)
        {
            var query = _applicationUserRepository.GetMulti(x => x.UserName.Contains(filter));
            if (string.IsNullOrEmpty(filter))
            {
                query = _applicationUserRepository.GetAll();
                totalRow = query.Count();
                return query.Skip((page - 1) * pageSize).Take(pageSize);
            }
            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void SetViewed(string id)
        {
            var user = _applicationUserRepository.GetSingleById(id);
            user.IsViewed = true;
            _applicationUserRepository.Update(user);
            _unitOfWork.Commit();

        }
    }
}