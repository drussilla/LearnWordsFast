using System;
using System.Collections.Generic;

namespace LearnWordsFast.ViewModels.UserController
{
    public class UpdateLanguagesViewModel
    {
        public Guid TrainingLanguage { get; set; }

        public Guid MainLanguage { get; set; }

        public List<Guid> AdditionalLanguages { get; set; }
    }
}