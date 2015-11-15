using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnWordsFast.DAL.Models
{
    public class Word : IdModel
    {
        public Word()
        {
            AdditionalTranslations = new List<Translation>();
            TrainingHistories = new List<TrainingHistory>();
        }
        
        public virtual string Original { get; set; }
        public virtual Language Language { get; set; }
        public virtual Translation Translation { get; set; }
        public virtual DateTime AddedDateTime { get; set; }
        public virtual string Context { get; set; }
        public virtual IList<Translation> AdditionalTranslations { get; set; } 
        public virtual IList<TrainingHistory> TrainingHistories { get; set; } 
        public virtual Guid UserId { get; set; }

        public virtual int TrainingAmout => TrainingHistories.Count;

        public virtual DateTime? LastTrainingDateTime => TrainingHistories.Count == 0 ? (DateTime?)null : TrainingHistories.Max(x => x.Date);
    }
}