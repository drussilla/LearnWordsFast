using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.ViewModels.TrainingController
{
    public class NoWordsTrainingViewModel : TrainingViewModel
    {
        public NoWordsTrainingViewModel() : base(NoWords.Type)
        {
        }
    }
}