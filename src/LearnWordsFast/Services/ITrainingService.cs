using System;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.ViewModels.PracticeController;

namespace LearnWordsFast.Services
{
    public interface ITrainingService
    { 
        TrainingViewModel CreateTraining(Guid userId);

        void FinishTraining(Word word, bool isCorrect, float score);
    }
}