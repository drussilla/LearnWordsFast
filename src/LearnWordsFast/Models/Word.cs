using System;

namespace LearnWordsFast.Models
{
    public class Word
    {
        public Word()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Original { get; set; }
        public string Translation { get; set; }
        public int TrainingAmout { get; set; }
        public DateTime? LastTrainingDateTime { get; set; }
        public DateTime AddedDateTime { get; set; }
    }
}