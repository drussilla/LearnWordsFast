using System;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.ViewModels.PracticeController;

namespace LearnWordsFast.Services
{
    public interface ITrainingService
    { 
        Word GetNextWord(Guid userId);

        TrainingViewModel CreateTraining(Word word);

        void FinishTraining(Word word, bool isCorrect, float score);
    }
}