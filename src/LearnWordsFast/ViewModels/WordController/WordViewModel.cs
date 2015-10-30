using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.ViewModels.WordController
{
    public class WordViewModel
    {
        public WordViewModel()
        {
        }

        public WordViewModel(Word word)
        {
            Id = word.Id;
            Original = word.Original;
            Language = word.Language.Id;
            Translation = new TranslationViewModel(word.Translation);
            Context = word.Context;
            if (word.AdditionalTranslations != null && word.AdditionalTranslations.Count > 0)
            {
                AdditionalTranslations = word.AdditionalTranslations.Select(x => new TranslationViewModel(x)).ToList();
            }
        }

        public Guid Id { get; set; }
        public string Original { get; set; }
        public Guid Language { get; set; }
        public TranslationViewModel Translation { get; set; }
        public string Context { get; set; }
        public List<TranslationViewModel> AdditionalTranslations { get; set; }
    }
}