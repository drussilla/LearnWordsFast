using System;
using System.Collections.Generic;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.ViewModels.TrainingController
{
    public class TrainingResultViewModel
    {
        public Guid WordId { get; set; } 

        public bool IsCorrect { get; set; }

        public TrainingType TrainingType { get; set; }
    }
}