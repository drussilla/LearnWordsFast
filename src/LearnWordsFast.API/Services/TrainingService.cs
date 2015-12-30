using System;
using LearnWordsFast.API.Exceptions;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.API.ViewModels.TrainingController;

namespace LearnWordsFast.API.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IWordRepository _wordRepository;
        private readonly ITrainingWordProvider _wordProvider;
        private readonly ITrainingSessionFactory _trainingSessionFactory;

        public TrainingService(
            IWordRepository wordRepository, 
            ITrainingWordProvider wordProvider,
            ITrainingSessionFactory trainingSessionFactory)
        {
            _wordRepository = wordRepository;
            _wordProvider = wordProvider;
            _trainingSessionFactory = trainingSessionFactory;
        }

        public TrainingViewModel CreateTraining(Guid userId)
        {
            var word = _wordProvider.Next(userId);
            if (word == null)
            {
                return new NoWordsTrainingViewModel();
            }

            return _trainingSessionFactory.Create(word);
        }

        public void FinishTraining(Guid userId, TrainingResultViewModel result)
        {
            var word = _wordRepository.Get(result.WordId, userId);
            if (word == null)
            {
                throw new NotFoundException();
            }

            word.TrainingHistories.Add(new TrainingHistory
            {
                Score = 100f,
                IsCorrect = result.IsCorrect,
                Date = DateTime.Now,
                Type = result.TrainingType 
            });

            _wordRepository.Update(word);
        }
    }
}