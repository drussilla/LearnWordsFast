using System;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.Services
{
    public interface ITrainingService
    { 
        Word GetNextWord(Guid userId);

        void FinishTraining(Word word);
    }
}