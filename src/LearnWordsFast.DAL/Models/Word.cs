using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnWordsFast.DAL.Models
{
    public class Word : IdModel
    {
        public Word()
        {
            AdditionalTranslations = new List<WordAdditionalTranslation>();
            TrainingHistories = new List<TrainingHistory>();
        }
        
        public string Original { get; set; }
        public Language Language { get; set; }
        public Guid LanguageId { get; set; }

        public virtual Translation Translation { get; set; }
        public Guid TranslationId { get; set; }
        public DateTime AddedDateTime { get; set; }

        public string Context { get; set; }

        public List<WordAdditionalTranslation> AdditionalTranslations { get; set; } 
        public List<TrainingHistory> TrainingHistories { get; set; }

        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }

        public virtual DateTime? LastCorrectTraining
        {
            get
            {
                if (TrainingHistories.Count == 0)
                {
                    return null;
                }

                var latestDate = DateTime.MinValue;
                foreach (var trainingHistory in TrainingHistories)
                {
                    if (trainingHistory.IsCorrect)
                    {
                        if (trainingHistory.Date > latestDate)
                        {
                            latestDate = trainingHistory.Date;
                        }
                    }
                }

                return latestDate;
            }
        }

        public virtual DateTime? LastTraining => TrainingHistories.Count == 0 ? (DateTime?)null : TrainingHistories.Max(x => x.Date);
    }
}