using System;
using System.Collections.Generic;
using System.Linq;
using LearnWordsFast.DAL.Models;

namespace LearnWordsFast.API.ViewModels.WordController
{
    public class WordViewModel
    {
        public WordViewModel()
        {
        }

        public WordViewModel(Word word)
        {
            Id = word.Id;
            Word = new TextViewModel(word.LanguageId, word.Original);
            Translation = new TextViewModel(word.Translation);
            Context = word.Context;
            if (word.AdditionalTranslations != null && word.AdditionalTranslations.Count > 0)
            {
                AdditionalTranslations = word.AdditionalTranslations.Select(x => new TextViewModel(x.Translation)).ToList();
            }
        }

        public Guid Id { get; set; }
        public TextViewModel Word { get; set; }
        public TextViewModel Translation { get; set; }
        public string Context { get; set; }
        public List<TextViewModel> AdditionalTranslations { get; set; }
    }
}