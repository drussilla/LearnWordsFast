using System;
using LearnWordsFast.API.ViewModels.TrainingController;

namespace LearnWordsFast.API.Services
{
    public interface ITrainingService
    { 
        TrainingViewModel CreateTraining(Guid userId);

        void FinishTraining(Guid userId, TrainingResultViewModel result);
    }
}