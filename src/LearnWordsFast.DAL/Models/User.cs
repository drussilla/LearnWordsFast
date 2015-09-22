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
        }

        public virtual Guid Id { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Language TrainingLanguage { get; set; }
        public virtual Language MainLanguage { get; set; }
        public virtual IList<Language> AdditionalLanguages { get; set; }
    }
}