using LearnWordsFast.DAL.Models;
using LearnWordsFast.API.ViewModels.TrainingController;

namespace LearnWordsFast.API.Services
{
    public interface ITrainingSessionFactory
    {
        TrainingViewModel Create(Word word);
    }
}