using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;

namespace LearnWordsFast.API.Services
{
    public class TrainingWordProvider : ITrainingWordProvider
    {
        private readonly IWordRepository _wordRepository;
        private readonly IDateTimeService _dateTimeService;

        // key - training amount
        // value - time to repeat again
        public static readonly Dictionary<int, TimeSpan> TrainingIntervals = new Dictionary<int, TimeSpan>
        {
            { 0, TimeSpan.Zero }, // if word is never trained train it immediately
            { 1, TimeSpan.FromHours(8) }, // if word was trained right once, train it againt only after 8 hours
            { 2, TimeSpan.FromDays(1) }, // if word was trained right 2 times, train it againt only after 1 day
            { 3, TimeSpan.FromDays(3) },
            { 4, TimeSpan.FromDays(6) },
            { 5, TimeSpan.FromDays(10) },
            { 6, TimeSpan.FromDays(25) }
        };

        public static readonly TimeSpan RetrainWrongInterval = TimeSpan.FromMinutes(15);

        public TrainingWordProvider(
            IWordRepository wordRepository,
            IDateTimeService dateTimeService)
        {
            _wordRepository = wordRepository;
            _dateTimeService = dateTimeService;
        }

        public Word Next(Guid userId)
        {
            var allWords = _wordRepository.GetAll(userId);

            if (allWords == null)
            {
                return null;
            }

            var stacks = allWords.GroupBy(x => x.TrainingHistories.Count(y => y.IsCorrect)).OrderBy(x => x.Key);
            Word wordReadyForTraining = null;
            DateTime olderDate = DateTime.MaxValue;
            foreach (var stack in stacks)
            {
                foreach (var word in stack)
                {
                    if (IsReadyForTraining(word, stack.Key))
                    {
                        if ((word.LastTraining == null && word.AddedDateTime < olderDate))
                        {
                            wordReadyForTraining = word;
                            olderDate = word.AddedDateTime;
                        }
                        else if (word.LastTraining != null && word.LastTraining < olderDate)
                        {
                            wordReadyForTraining = word;
                            olderDate = word.LastTraining.Value;
                        }
                    }
                }
            }

            return wordReadyForTraining;
        }

        private bool IsReadyForTraining(Word word, int stack)
        {
            // if word was trained more then amount of stacks, threat it as word from last stack
            if (stack >= TrainingIntervals.Count)
            {
                stack = TrainingIntervals.Count - 1;
            }

            var freezInStackInteval = TrainingIntervals[stack];

            if (stack == 0)
            {
                if (word.LastTraining != null)
                {
                    if (_dateTimeService.Now - word.LastTraining >= RetrainWrongInterval)
                    {
                        return true;
                    }
                }

                if (_dateTimeService.Now - word.AddedDateTime >= freezInStackInteval)
                {
                    return true;
                }
            }

            if (word.LastCorrectTraining != word.LastTraining)
            {
                if (_dateTimeService.Now - word.LastCorrectTraining > RetrainWrongInterval)
                {
                    return true;
                }
            }

            if (_dateTimeService.Now - word.LastCorrectTraining > freezInStackInteval)
            {
                return true;
            }

            return false;
        }
    }
}