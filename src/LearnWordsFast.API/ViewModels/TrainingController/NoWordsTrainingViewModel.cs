using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.API.ViewModels.TrainingController
{
    public class NoWordsTrainingViewModel : TrainingViewModel
    {
        public NoWordsTrainingViewModel() : base(NoWords.NoWord)
        {
        }
    }
}