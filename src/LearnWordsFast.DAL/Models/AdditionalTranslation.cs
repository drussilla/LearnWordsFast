using System;

namespace LearnWordsFast.DAL.Models
{
    public class AdditionalTranslation
    {
        public AdditionalTranslation()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
        public virtual Word Word { get; set; }
        public virtual Language Language { get; set; }
        public virtual string Translation { get; set; }
    }
}