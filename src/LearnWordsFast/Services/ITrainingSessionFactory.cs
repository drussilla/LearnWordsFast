using LearnWordsFast.DAL.Models;
using LearnWordsFast.ViewModels.TrainingController;

namespace LearnWordsFast.Services
{
    public interface ITrainingSessionFactory
    {
        TrainingViewModel Create(Word word);
    }
}