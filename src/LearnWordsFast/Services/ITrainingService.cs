using System;
using LearnWordsFast.ViewModels.TrainingController;

namespace LearnWordsFast.Services
{
    public interface ITrainingService
    { 
        TrainingViewModel CreateTraining(Guid userId);

        void FinishTraining(Guid userId, TrainingResultViewModel result);
    }
}