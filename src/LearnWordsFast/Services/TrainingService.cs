using System;
using System.Linq;
using System.Security.Policy;
using LearnWordsFast.Models;
using LearnWordsFast.Repositories;

namespace LearnWordsFast.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IWordRepository wordRepository;
        private readonly IDateTimeService dateTimeService;

        public TrainingService(IWordRepository wordRepository, IDateTimeService dateTimeService)
        {
            this.wordRepository = wordRepository;
            this.dateTimeService = dateTimeService;
        }

        public Word GetNextWord()
        {
            return wordRepository.GetAll().OrderBy(x => x.AddedDateTime).FirstOrDefault();
        }

        public void FinishTraining(Word word)
        {
            word.LastTrainingDateTime = dateTimeService.Now;
            word.TrainingAmout++;
            wordRepository.Update(word);
        }
    }
}