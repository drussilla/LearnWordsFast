using System;

namespace LearnWordsFast.DAL.Models
{
    public class TrainingHistory : IdModel
    {
        public virtual bool IsCorrect { get; set; }
        public virtual float Score { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual TrainingType Type { get; set; }
    }
}