using System;

namespace LearnWordsFast.Models
{
    public class Word
    {
        public string Original { get; set; }
        public string Translation { get; set; }
        public DateTime? LastTrainingTime { get; set; }
    }
}