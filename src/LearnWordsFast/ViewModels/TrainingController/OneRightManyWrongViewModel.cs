using System.Collections.Generic;
using LearnWordsFast.DAL.Models;
using LearnWordsFast.ViewModels.WordController;

namespace LearnWordsFast.ViewModels.TrainingController
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