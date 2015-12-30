using LearnWordsFast.API.ViewModels.TrainingController;
using LearnWordsFast.API.ViewModels.WordController;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.API.ViewModels.TrainingController
{
    public class OneRightTrainingViewModel : TrainingViewModel
    {
        public OneRightTrainingViewModel(OneRight type, WordViewModel word) : base(type)
        {
            Word = word;
        }

        public WordViewModel Word { get; private set; }
    }
}