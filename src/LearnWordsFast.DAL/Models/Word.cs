using System;

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
    }
}