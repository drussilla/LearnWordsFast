using System;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.API.ViewModels.WordController
{
    public class TranslationViewModel
    {
        public TranslationViewModel()
        {
        }

        public TranslationViewModel(Translation model)
        {
            Language = model.LanguageId;
            Translation = model.TranslationText;
        }

        public Guid Language { get; set; } 
        public string Translation { get; set; }

        public Translation ToModel()
        {
            return new Translation
            {
                LanguageId = Language,
                TranslationText = Translation
            };
        }
    }
}