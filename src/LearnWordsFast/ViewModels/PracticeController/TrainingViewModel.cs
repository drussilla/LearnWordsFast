using System;
using System.Collections.Generic;
using LearnWordsFast.ViewModels.WordController;

namespace LearnWordsFast.ViewModels.PracticeController
{
    public class TrainingViewModel
    {
        public TrainingViewModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public TrainingType Type { get; set; } 
        public List<WordViewModel> Words { get; set; }
    }
}