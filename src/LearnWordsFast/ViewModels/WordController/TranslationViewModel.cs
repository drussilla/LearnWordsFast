using System;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.ViewModels.WordController
{
    public class TranslationViewModel
    {
        public TranslationViewModel()
        {
        }

        public TranslationViewModel(Translation model)
        {
            Language = model.Language.Id;
            Translation = model.TranslationText;
        }

        public Guid Language { get; set; } 
        public string Translation { get; set; }

        public Translation ToModel()
        {
            return new Translation()
            {
                Language = new Language(Language),
                TranslationText = Translation
            };
        }
    }
}