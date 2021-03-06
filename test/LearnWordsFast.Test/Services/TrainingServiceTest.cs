﻿// TODO: update test
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using FluentAssertions;
//using LearnWordsFast.DAL.Models;
//using LearnWordsFast.DAL.Repositories;
//using LearnWordsFast.Services;
//using Moq;
//using Ploeh.AutoFixture.Xunit2;
//using Xunit;

//namespace LearnWordsFast.Test.Services
//{
//    public class TrainingServiceTest
//    {
//        private static DateTime now = DateTime.Now;

//        [Theory, MemberData("GetWords")]
//        public void GetNextWord_ReturnWordThatWasTrainedLongerThenSpecifiedForAmountOfTraining(List<Word> words, int expectedWordIndex)
//        {
//            var dateTimeService = new Mock<IDateTimeService>();
//            dateTimeService
//                .Setup(x => x.Now)
//                .Returns(now);

//            var wordRepository = new Mock<IWordRepository>();
//            wordRepository
//                .Setup(x => x.GetAll(It.IsAny<Guid>()))
//                .Returns(words);

//            var target = new TrainingService(wordRepository.Object, dateTimeService.Object);
//            var result = target.GetNextWord(Guid.Empty);

//            if (expectedWordIndex == -1)
//            {
//                Assert.Null(result);
//            }
//            else
//            {
//                Assert.Equal(words[expectedWordIndex], result);
//            }
//        }

//        public static IEnumerable GetWords()
//        {
//            // no words for training
//            yield return new object[]
//            {
//                new List<Word>
//                {
//                    NotReadyForTrainingWord(1),
//                    NotReadyForTrainingWord(1),
//                    NotReadyForTrainingWord(2)
//                },
//                -1
//            };

//            yield return new object[]
//            {
//                // word without repetion always first
//                new List<Word>
//                {
//                    ReadyForTrainingWord(0),
//                    ReadyForTrainingWord(1),
//                    ReadyForTrainingWord(2)
//                },
//                0
//            };

//            yield return new object[]
//            {
//                // then word with lower amount of repetition wins
//                new List<Word>
//                {
//                    NotReadyForTrainingWord(1),
//                    ReadyForTrainingWord(1),
//                    ReadyForTrainingWord(1),
//                    ReadyForTrainingWord(2)
//                },
//                1
//            };

//            yield return new object[]
//            {
//                new List<Word>
//                {
//                    NotReadyForTrainingWord(1),
//                    NotReadyForTrainingWord(1),
//                    NotReadyForTrainingWord(1),
//                    ReadyForTrainingWord(2),
//                    ReadyForTrainingWord(3)
//                },
//                3
//            };

//            yield return new object[]
//            {
//                new List<Word>
//                {
//                    ReadyForTrainingWord(5),
//                    NotReadyForTrainingWord(5),
//                    ReadyForTrainingWord(5)
//                },
//                0
//            };

//            // if words with the same trainging amount, then word which was trained earlier then othes
//            yield return new object[]
//            {
//                new List<Word>
//                {
//                    new Word { AddedDateTime = now, LastTrainingDateTime = now.AddYears(-1), },
//                    new Word { AddedDateTime = now, LastTrainingDateTime = now.AddYears(-2), }
//                },
//                1
//            };

//            yield return new object[]
//            {
//                new List<Word>
//                {
//                    new Word { AddedDateTime = now, LastTrainingDateTime = now.AddYears(-1), },
//                    new Word { AddedDateTime = now, LastTrainingDateTime = now.AddYears(-2), }
//                },
//                1
//            };
//        }

//        private static Word ReadyForTrainingWord(int trainingAmount)
//        {
//            return new Word
//            {
//                AddedDateTime = now.AddYears(-100),
//                Original = Guid.NewGuid().ToString(),
//                TrainingAmout = trainingAmount,
//                Id = Guid.NewGuid(),
//                LastTrainingDateTime =
//                    trainingAmount == 0
//                        ? null
//                        : (DateTime?)
//                            (now - TrainingService.TrainingIntervals[trainingAmount] - TimeSpan.FromMinutes(10))
//            };
//        }

//        private static Word NotReadyForTrainingWord()
//        {
//            return new Word
//            {
//                AddedDateTime = now.AddYears(-100),
//                Original = Guid.NewGuid().ToString(),
//                Id = Guid.NewGuid(),
//                LastTrainingDateTime =
//                    trainingAmount == 0
//                        ? null
//                        : (DateTime?)
//                            (now - TrainingService.TrainingIntervals[trainingAmount] + TimeSpan.FromMinutes(10))
//            };
//        }

//        [Theory, AutoMoqData]
//        public void FinishTraining_AmountOfTrainingsIncreasedAndLastTrainingDateUpdated(
//            [Frozen] Mock<IWordRepository>wordRepository,
//            [Frozen] Mock<IDateTimeService> dateTimeService,
//            TrainingService target)
//        {
//            dateTimeService
//                .Setup(x => x.Now)
//                .Returns(now);

//            var word = ReadyForTrainingWord(1);
//            target.FinishTraining(word, true, 50f);

//            wordRepository.Verify(x => x.Update(word), Times.Once);

//            word.TrainingHistories.Should().HaveCount(1);
//            Assert.Equal(2, word.TrainingAmout);
//            Assert.Equal(now, word.LastTrainingDateTime);
//        }
//    }
//}