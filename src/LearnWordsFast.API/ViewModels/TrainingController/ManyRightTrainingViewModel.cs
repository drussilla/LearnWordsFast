using System.Collections.Generic;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.API.ViewModels.TrainingController;
using LearnWordsFast.API.ViewModels.WordController;

namespace LearnWordsFast.API.ViewModels.TrainingController
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