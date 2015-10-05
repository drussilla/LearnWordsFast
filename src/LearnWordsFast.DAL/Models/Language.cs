using System;

namespace LearnWordsFast.DAL.Models
{
    public class Language : IdModel
    {
        public Language(Guid id)
        {
            Id = id;
        }

        public Language()
        {
        }
        
        public virtual string Name { get; set; }
    }
}