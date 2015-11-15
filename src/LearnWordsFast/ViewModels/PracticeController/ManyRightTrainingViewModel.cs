using System.Collections.Generic;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.ViewModels.WordController;

namespace LearnWordsFast.ViewModels.PracticeController
{
    public class ManyRightTrainingViewModel : TrainingViewModel
    {
        public ManyRightTrainingViewModel(ManyRight type, IList<WordViewModel> words) : base(type)
        {
            Words = words;
        }

        public IList<WordViewModel> Words { get; set; } 
    }
}