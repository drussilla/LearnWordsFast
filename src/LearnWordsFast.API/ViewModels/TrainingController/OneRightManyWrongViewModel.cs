using System.Collections.Generic;
using LearnWordsFast.API.ViewModels.TrainingController;
using LearnWordsFast.API.ViewModels.WordController;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.API.ViewModels.TrainingController
{
    public class OneRightManyWrongViewModel : TrainingViewModel
    {
        public OneRightManyWrongViewModel(OneRightManyWrong type, WordViewModel right, IList<WordViewModel> wrong) : base(type)
        {
            RightWord = right;
            WrongWords = wrong;
        }

        public WordViewModel RightWord { get; private set; }

        public IList<WordViewModel> WrongWords { get; private set; }
    }
}