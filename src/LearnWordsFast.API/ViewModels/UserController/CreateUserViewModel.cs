using System;
using System.Collections.Generic;

namespace LearnWordsFast.API.ViewModels.UserController
{
    public class CreateUserViewModel
    {
        public string Email { get; set; } 
        public string Password { get; set; }
        public Guid TrainingLanguage { get; set; }
        public Guid MainLanguage { get; set; }
        public IList<Guid> AdditionalLanguages { get; set; }
    }
}