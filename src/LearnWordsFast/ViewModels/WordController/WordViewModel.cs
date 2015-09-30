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
        }

        public string Original { get; set; }
        public string Translation { get; set; }
        public string Context { get; set; }
    }
}