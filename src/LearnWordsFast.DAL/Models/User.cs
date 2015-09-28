using System;
using System.Collections;
using System.Collections.Generic;

namespace LearnWordsFast.DAL.Models
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            AdditionalLanguages = new List<Language>();
            Words = new List<Word>();
        }

        public virtual Guid Id { get; protected set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Language TrainingLanguage { get; set; }
        public virtual Language MainLanguage { get; set; }
        public virtual IList<Language> AdditionalLanguages { get; protected set; }
        public virtual IList<Word> Words { get; protected set; }
    }
}