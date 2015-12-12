using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.ViewModels.UserController
{
    public class UserViewModel
    {
        public UserViewModel(User user)
        {
            Email = user.Email;
            TrainingLanguage = user.TrainingLanguage.Id;
            MainLanguage = user.MainLanguage.Id;
            AdditionalLanguages = user.AdditionalLanguages.Select(x => x.LanguageId).ToList();
        }

        public string Email { get; private set; }

        public Guid TrainingLanguage { get; private set; }

        public Guid MainLanguage { get; private set; }

        public List<Guid> AdditionalLanguages { get; private set; }
    }
}