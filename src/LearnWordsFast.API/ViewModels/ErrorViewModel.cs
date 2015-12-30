using System.Collections.Generic;

namespace LearnWordsFast.API.ViewModels
{
    public class ErrorViewModel
    {
        public ErrorViewModel()
        {
            Errors = new List<string>();
        }

        public ErrorViewModel(IEnumerable<string> errors)
        {
            Errors = new List<string>(errors);
        }

        public List<string> Errors { get; set; }
    }
}