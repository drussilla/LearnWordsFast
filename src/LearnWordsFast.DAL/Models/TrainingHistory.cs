using System;

namespace LearnWordsFast.DAL.Models
{
    public class TrainingHistory : IdModel
    { 
        public virtual Word Word { get; set; }
        public virtual Guid WordId { get; set; }
        public virtual bool IsCorrect { get; set; }
        public virtual float Score { get; set; }
    }
}