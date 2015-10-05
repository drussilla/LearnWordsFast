using System;
using System.Collections.Generic;

namespace LearnWordsFast.DAL.Models
{
    public class Word
    {
        public Word()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
        public virtual string Original { get; set; }
        public virtual string Translation { get; set; }
        public virtual int TrainingAmout { get; set; }
        public virtual DateTime? LastTrainingDateTime { get; set; }
        public virtual DateTime AddedDateTime { get; set; }
        public virtual string Context { get; set; }
        public virtual List<AdditionalTranslation> AdditionalTranslations { get; set; } 

        public virtual Guid UserId { get; set; }
    }
}