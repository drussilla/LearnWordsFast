using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.ViewModels.WordController
{
    public class WordViewModel
    {
        public WordViewModel(Word word)
        {
            Original = word.Original;
            Translation = word.Translation;
            Context = word.Context;
            if (word.AdditionalTranslations != null && word.AdditionalTranslations.Count > 0)
            {
                AdditionalTranslations = word.AdditionalTranslations.Select(x => new AdditionalLanguageViewModel
                {
                    Language = x.Language.Id,
                    Translation = x.Translation
                }).ToList();
            }
        }

        public string Original { get; set; }
        public string Translation { get; set; }
        public string Context { get; set; }
        public List<AdditionalLanguageViewModel> AdditionalTranslations { get; set; }
    }
}