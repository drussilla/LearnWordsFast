﻿using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.Models;
using LearnWordsFast.Repositories;

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
            { 1, TimeSpan.FromMinutes(30) }, // if word was trained once, train it againt only after 30 minutes
            { 2, TimeSpan.FromHours(8) }, // if word was trained 2 times, train it againt only after 8 hours
            { 3, TimeSpan.FromDays(1) },
            { 4, TimeSpan.FromDays(21) },
            { 5, TimeSpan.FromDays(60) }
        }; 

        public TrainingService(IWordRepository wordRepository, IDateTimeService dateTimeService)
        {
            this.wordRepository = wordRepository;
            this.dateTimeService = dateTimeService;
        }

        public Word GetNextWord()
        {
            var wordGroup = wordRepository.GetAll().GroupBy(x => x.TrainingAmout).OrderBy(x => x.Key);
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

        public void FinishTraining(Word word)
        {
            word.LastTrainingDateTime = dateTimeService.Now;
            word.TrainingAmout++;
            wordRepository.Update(word);
        }
    }
}