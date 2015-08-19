using System;
using System.Collections.Generic;
using LearnWordsFast.Models;
using LearnWordsFast.Repositories;
using LearnWordsFast.Services;
using Moq;
using Ploeh.AutoFixture.Xunit2;
using Xunit;
using ZohoPeopleTimeLogger.UnitTests;

namespace LearnWordsFast.Test.Services
{
    public class TrainingServiceTest
    {
        [Theory, AutoMoqData]
        public void GetNextWord_NoWordsWithTraining_ReturnOlder(
            Word word1,
            Word word2,
            Word word3,
            [Frozen] Mock<IWordRepository> wordRepository,
            TrainingService target)
        {
            word1.AddedDateTime = new DateTime(2015, 01, 01);
            word2.AddedDateTime = new DateTime(2015, 02, 01);
            word3.AddedDateTime = new DateTime(2015, 03, 01);

            wordRepository
                .Setup(x => x.GetAll())
                .Returns(new List<Word> {word1, word2, word3});

            var first = target.GetNextWord();
            
            Assert.Equal(word1, first);
        }
    }
}