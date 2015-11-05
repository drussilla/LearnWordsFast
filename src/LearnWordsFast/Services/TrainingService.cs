using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.ViewModels.PracticeController;
using LearnWordsFast.ViewModels.WordController;

namespace LearnWordsFast.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly IWordRepository wordRepository;
        private readonly IDateTimeService dateTimeService;

        // key - training amount
        // value - time to repeat again
        public static readonly Dictionary<int, TimeSpan> TrainingIntervals = new Dictionary<int, TimeSpan>
        { 
            { 0, TimeSpan.Zero }, // if word is never trained train it immediately
            { 1, TimeSpan.FromHours(8) }, // if word was trained once, train it againt only after 30 minutes
            { 2, TimeSpan.FromDays(1) }, // if word was trained 2 times, train it againt only after 8 hours
            { 3, TimeSpan.FromDays(2) },
            { 4, TimeSpan.FromDays(4) },
            { 5, TimeSpan.FromDays(10) }
        }; 

        public TrainingService(IWordRepository wordRepository, IDateTimeService dateTimeService)
        {
            this.wordRepository = wordRepository;
            this.dateTimeService = dateTimeService;
        }

        public Word GetNextWord(Guid userId)
        {
            var wordGroup = wordRepository.GetAll(userId).GroupBy(x => x.TrainingAmout).OrderBy(x => x.Key);
            foreach (var group in wordGroup)
            {
                // group 0 contains just added words (no training performaed at all)
                if (group.Key == 0)
                {
                    return group.FirstOrDefault();
                }

                // all other words that were trained mothe than 5 times. Retrain them only if no other words left for training
                if (group.Key > 5)
                {
                    return group.OrderBy(x => x.LastTrainingDateTime).FirstOrDefault();
                }

                var trainingInvervalForGroup = TrainingIntervals[group.Key];
                foreach (var word in group.OrderBy(x => x.LastTrainingDateTime))
                {
                    if (dateTimeService.Now - word.LastTrainingDateTime > trainingInvervalForGroup)
                    {
                        return word;
                    }
                }
            }

            return null;
        }

        public TrainingViewModel CreateTraining(Word word)
        {
            if (word.TrainingAmout == 0)
            {
                return new TrainingViewModel
                {
                    Type = TrainingType.TranslateFromLearnToOriginal,
                    Words = new List<WordViewModel> {new WordViewModel(word)}
                };
            }

            return new TrainingViewModel
            {
                Type = TrainingType.TranslateFromOriginalToLearn,
                Words = new List<WordViewModel> {new WordViewModel(word)}
            };
        }

        public void FinishTraining(Word word)
        {
            word.LastTrainingDateTime = dateTimeService.Now;
            word.TrainingAmout++;
            wordRepository.Update(word);
        }
    }
}