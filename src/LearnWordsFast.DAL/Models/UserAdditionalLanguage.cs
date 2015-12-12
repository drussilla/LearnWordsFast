using System;

namespace LearnWordsFast.DAL.Models
{
    public class UserAdditionalLanguage
    {
        public User User { get; set; } 
        public Guid UserId { get; set; }

        public Language Language { get; set; }
        public Guid LanguageId { get; set; }
    }
}