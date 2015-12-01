using FluentAssertions;
using LearnWordsFast.DAL.Repositories;
using LearnWordsFast.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;
using Xunit;

namespace LearnWordsFast.Test.Services
{
    public class TrainingWordProviderTest
    {
        [Fact]
        public void Next_NullList_ReturnNull()
        {
            TestNext(DateTime.Now);
        }

        [Fact]
        public void Next_OnlyNotTrainedWords_ReturnTheOldest()
        {
            TestNext(
                new DateTime(2010, 02, 05),
                Word(2010, 02, 01),
                Word(2010, 02, 02),
                Word(2010, 02, 03),
                Word(2010, 01, 01));
        }

        [Fact]
        public void Next_TrainedOnceButNotReadyForSecond_ReturnNull()
        {
            TestNext(
                new DateTime(2010, 01, 01, 13, 00, 00),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 12, 12),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 33, 12),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 44, 12),
                null);
        }

        [Fact]
        public void Next_TrainedOnceAndReadyForSecond_ReturnTheOldest()
        {
            TestNext(
                new DateTime(2010, 01, 02, 13, 00, 00),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 12, 12),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 33, 12),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 00, 12));
        }

        [Fact]
        public void Next_TrainedWrongAndReady_ReturnTheOldest()
        {
            TestNext(
                new DateTime(2010, 01, 01, 12, 20, 00),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 00, 10),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 00, 11),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 00, 12)
                    .TrainF(2010, 01, 01, 12, 00, 13));
        }

        [Fact]
        public void Next_TrainedOnlyWrongAndReady_ReturnTheOldest()
        {
            TestNext(
                new DateTime(2010, 01, 01, 12, 20, 00),
                Word(2010, 01, 01, 12, 19, 00)
                    .TrainF(2010, 01, 01, 12, 01, 10),
                Word(2010, 01, 01, 12, 19, 01)
                    .TrainF(2010, 01, 01, 12, 01, 11),
                Word(2010, 01, 01, 12, 19, 02)
                    .TrainF(2010, 01, 01, 12, 00, 12)
                    .TrainF(2010, 01, 01, 12, 00, 13));
        }

        [Fact]
        public void Next_TrainedDifferentTimesAndReady_ReturnTheOldest()
        {
            TestNext(
                new DateTime(2010, 01, 02, 13, 00, 00),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 12, 12)
                    .TrainF(2010, 01, 01, 12, 12, 13)
                    .TrainT(2010, 01, 01, 12, 12, 14)
                    .TrainT(2010, 01, 01, 12, 12, 15),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 33, 12)
                    .TrainT(2010, 01, 01, 12, 12, 12)
                    .TrainT(2010, 01, 01, 12, 12, 12),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 00, 12)
                    .TrainF(2010, 01, 01, 12, 00, 13)
                    .TrainT(2010, 01, 01, 12, 00, 14));
        }

        [Fact]
        public void Next_TrainedALotOfTimesAndReadyForTraining_ReturnTheOldest()
        {
            TestNext(
                new DateTime(2010, 01, 26, 13, 00, 00),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 12, 12)
                    .TrainT(2010, 01, 01, 12, 12, 13)
                    .TrainT(2010, 01, 01, 12, 12, 14)
                    .TrainT(2010, 01, 01, 12, 12, 15)
                    .TrainT(2010, 01, 01, 12, 12, 16)
                    .TrainT(2010, 01, 01, 12, 12, 17)
                    .TrainT(2010, 01, 01, 12, 12, 18)
                    .TrainT(2010, 01, 01, 12, 12, 19)
                    .TrainT(2010, 01, 01, 12, 12, 20),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 11, 12)
                    .TrainT(2010, 01, 01, 12, 11, 13)
                    .TrainT(2010, 01, 01, 12, 11, 14)
                    .TrainT(2010, 01, 01, 12, 11, 15)
                    .TrainT(2010, 01, 01, 12, 11, 16)
                    .TrainT(2010, 01, 01, 12, 11, 17)
                    .TrainT(2010, 01, 01, 12, 11, 18)
                    .TrainT(2010, 01, 01, 12, 11, 19)
                    .TrainT(2010, 01, 01, 12, 11, 20));
        }

        [Fact]
        public void Next_TrainedALotOfTimesButNotReadyForTraining_ReturnNull()
        {
            TestNext(
                new DateTime(2010, 01, 24, 13, 00, 00),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 12, 12)
                    .TrainT(2010, 01, 01, 12, 12, 13)
                    .TrainT(2010, 01, 01, 12, 12, 14)
                    .TrainT(2010, 01, 01, 12, 12, 15)
                    .TrainT(2010, 01, 01, 12, 12, 16)
                    .TrainT(2010, 01, 01, 12, 12, 17)
                    .TrainT(2010, 01, 01, 12, 12, 18)
                    .TrainT(2010, 01, 01, 12, 12, 19)
                    .TrainT(2010, 01, 01, 12, 12, 20),
                Word(2010, 01, 01)
                    .TrainT(2010, 01, 01, 12, 11, 12)
                    .TrainT(2010, 01, 01, 12, 11, 13)
                    .TrainT(2010, 01, 01, 12, 11, 14)
                    .TrainT(2010, 01, 01, 12, 11, 15)
                    .TrainT(2010, 01, 01, 12, 11, 16)
                    .TrainT(2010, 01, 01, 12, 11, 17)
                    .TrainT(2010, 01, 01, 12, 11, 18)
                    .TrainT(2010, 01, 01, 12, 11, 19)
                    .TrainT(2010, 01, 01, 12, 11, 20),
                null);
        }

        private void TestNext(DateTime now, params Word[] words)
        {
            Word expected;
            List<Word> wordList = null;
            if (words == null || words.Length == 0)
            {
                expected = null;
            }
            else
            {
                wordList = words.ToList();
                expected = wordList.Last();

                if (expected == null)
                {
                    wordList.RemoveAt(wordList.Count - 1);
                }
            }

            Guid userId = Guid.NewGuid();

            var wordRepo = new Mock<IWordRepository>();
            wordRepo
                .Setup(x => x.GetAll(userId))
                .Returns(wordList);

            var dateTimeSrv = new Mock<IDateTimeService>();
            dateTimeSrv
                .Setup(x => x.Now)
                .Returns(now);

            var target = new TrainingWordProvider(wordRepo.Object, dateTimeSrv.Object);

            var actual = target.Next(userId);

            actual.Should().Be(expected);
        }

        private static Word Word(int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        {
            return new Word {AddedDateTime = new DateTime(year, month, day, hour, minute, second)};
        }
    }

    public static class WordTestEx
    {
        public static Word TrainT(this Word word, int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        {
            return word.Train(true, year, month, day, hour, minute, second);
        }

        public static Word TrainF(this Word word, int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        {
            return word.Train(false, year, month, day, hour, minute, second);
        }

        public static Word Train(this Word word, bool isCorrect, int year, int month, int day, int hour = 0, int minute = 0, int second = 0)
        {
            word.TrainingHistories.Add(new TrainingHistory
            {
                IsCorrect = isCorrect,
                Date = new DateTime(year, month, day, hour, minute, second)
            });

            return word;
        }
    }
}