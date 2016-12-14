using System;
using System.Collections.Generic;
using System.Linq;
using ObajuStore.Data.Infrastructure;
using ObajuStore.Data.Repositories;
using ObajuStore.Model.Models;

namespace ObajuStore.Service
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);
        void Update(Feedback feedback);
        IEnumerable<Feedback> GetAll(string keyword);
        Feedback GetSingleById(int id);
        Feedback Delete(int id);
        void SaveChanges();
    }

    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this._feedbackRepository = feedbackRepository;
            this._unitOfWork = unitOfWork;
        }

        public Feedback Create(Feedback feedback)
        {
            feedback.Status = true;
            return _feedbackRepository.Add(feedback);
        }

        public Feedback Delete(int id)
        {
            return _feedbackRepository.Delete(id);
        }

        public IEnumerable<Feedback> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _feedbackRepository.GetMulti(x => x.Name.Contains(keyword)).OrderByDescending(y => y.CreatedDate);
            }
            else
            {
                return _feedbackRepository.GetAll();
            }
        }

        public Feedback GetSingleById(int id)
        {
            return _feedbackRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Feedback feedback)
        {
            _feedbackRepository.Update(feedback);
        }
    }
}