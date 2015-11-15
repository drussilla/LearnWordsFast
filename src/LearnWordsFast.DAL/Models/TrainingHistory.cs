using System;

namespace LearnWordsFast.DAL.Models
{
    public class TrainingHistory : IdModel
    { 
        public virtual bool IsCorrect { get; set; }
        public virtual float Score { get; set; }
    }
}