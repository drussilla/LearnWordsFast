using System.Collections.Generic;

namespace LearnWordsFast.ViewModels.WordController
{
    public class CreateWordViewModel
    {
        public string Original { get; set; }
        public TranslationViewModel Translation { get; set; }
        public string Context { get; set; }
        public List<TranslationViewModel> AdditionalTranslations { get; set; }
    }
}