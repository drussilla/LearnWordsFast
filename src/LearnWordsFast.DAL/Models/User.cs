using System;
using System.Collections.Generic;

namespace LearnWordsFast.DAL.Models
{
    public class User : IdModel
    {
        public User()
        {
            AdditionalLanguages = new List<UserAdditionalLanguage>();
            Words = new List<Word>();
        }
        
        public string Email { get; set; }
        public string Password { get; set; }

        public Language TrainingLanguage { get; set; }
        public Guid TrainingLanguageId { get; set; }

        public Language MainLanguage { get; set; }
        public Guid MainLanguageId { get; set; }

        public List<UserAdditionalLanguage> AdditionalLanguages { get; protected set; }

        public List<Word> Words { get; protected set; }
    }
}