using System;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.API.ViewModels.TrainingController
{
    public class TrainingResultViewModel
    {
        public Guid WordId { get; set; } 

        public bool IsCorrect { get; set; }

        public TrainingType TrainingType { get; set; }
    }
}