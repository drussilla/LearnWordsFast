using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Infrastructure;
using LearnWordsFast.ViewModels.TrainingController;
using LearnWordsFast.ViewModels.WordController;

namespace LearnWordsFast.Services
{
    public class TrainingSessionFactory : ITrainingSessionFactory
    {
        private readonly IWordRepository _wordRepository;

        public TrainingSessionFactory(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public TrainingViewModel Create(Word rightWord)
        {
            var stackIndex = rightWord.TrainingHistories.Count(x => x.IsCorrect);
            var rightWordViewModel = new WordViewModel(rightWord);
            switch (stackIndex)
            {
                case 0:
                {
                    return new OneRightTrainingViewModel(OneRight.RepeatTranslation, rightWordViewModel);
                }
                case 1:
                {
                    return new OneRightManyWrongViewModel(
                        OneRightManyWrong.ChooseTranslation,
                        rightWordViewModel, 
                        GetRandomWords(except: rightWord, amount: 5));
                }
                case 2:
                {
                    return new OneRightManyWrongViewModel(
                        OneRightManyWrong.ChooseOriginal,
                        rightWordViewModel,
                        GetRandomWords(except: rightWord, amount: 5));
                }
                case 3:
                {
                    var words = GetRandomWords(except: rightWord, amount: 4);
                    words.Add(rightWordViewModel);
                    return new ManyRightTrainingViewModel(ManyRight.MatchWords, words);
                }
                case 4:
                {
                    return new OneRightTrainingViewModel(OneRight.ComposeOriginal, rightWordViewModel);
                }
                case 5:
                {
                    return new OneRightTrainingViewModel(OneRight.TypeTranslation, rightWordViewModel);
                }
                case 6:
                {
                    return new OneRightTrainingViewModel(OneRight.TypeOriginal, rightWordViewModel);
                }
                default:
                {
                    return new OneRightTrainingViewModel(OneRight.TypeOriginal, rightWordViewModel);
                }
            }
        }

        private IList<WordViewModel> GetRandomWords(Word except, int amount)
        {
            var allWords = _wordRepository.GetAll(except.UserId).Except(new List<Word>
            {
                except
            }).ToList();

            if (allWords.Count <= amount)
            {
                return allWords.Select(x => new WordViewModel(x)).ToList();
            }

            return allWords.Shuffle().Take(amount).Select(x => new WordViewModel(x)).ToList();
        }
    }
}