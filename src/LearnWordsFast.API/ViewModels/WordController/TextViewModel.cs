using System;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.API.ViewModels.WordController
{
    public class TextViewModel
    {
        public TextViewModel()
        {
        }

        public TextViewModel(Guid language, string translation)
        {
            Language = language;
            Text = translation;
        }

        public TextViewModel(Translation model)
        {
            Language = model.LanguageId;
            Text = model.TranslationText;
        }

        public Guid Language { get; set; } 
        public string Text { get; set; }

        public Translation ToModel()
        {
            return new Translation
            {
                LanguageId = Language,
                TranslationText = Text
            };
        }

        public override string ToString()
        {
            return $"{Text}.{Language}";
        }
    }
}