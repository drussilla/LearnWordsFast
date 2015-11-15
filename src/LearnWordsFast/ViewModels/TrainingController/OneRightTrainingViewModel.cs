using LearnWordsFast.DAL.Models;
using LearnWordsFast.ViewModels.WordController;

namespace LearnWordsFast.ViewModels.TrainingController
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