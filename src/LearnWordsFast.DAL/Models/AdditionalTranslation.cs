using System;

namespace LearnWordsFast.DAL.Models
{
    public class AdditionalTranslation : IdModel
    {
        public virtual Word Word { get; set; }
        public virtual Language Language { get; set; }
        public virtual string Translation { get; set; }
    }
}