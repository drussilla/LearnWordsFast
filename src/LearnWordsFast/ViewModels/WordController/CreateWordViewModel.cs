using System.Collections.Generic;

namespace LearnWordsFast.ViewModels.WordController
{
    public class CreateWordViewModel
    {
        public string Original { get; set; }
        public string Translation { get; set; }
        public string Context { get; set; }
        public List<AdditionalLanguageViewModel> AdditionalTranslations { get; set; }
    }
}