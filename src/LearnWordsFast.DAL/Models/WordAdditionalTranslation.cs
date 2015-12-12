using System;

namespace LearnWordsFast.DAL.Models
{
    public class WordAdditionalTranslation
    {
        public Word Word { get; set; } 
        public Guid WordId { get; set; }

        public Translation Translation { get; set; }
        public Guid TranslationId { get; set; }
    }
}