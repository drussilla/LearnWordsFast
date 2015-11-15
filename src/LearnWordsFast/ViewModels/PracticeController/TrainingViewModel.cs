using LearnWordsFast.DAL.Models;
using LearnWordsFast.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LearnWordsFast.ViewModels.PracticeController
{
    public abstract class TrainingViewModel
    {
        protected TrainingViewModel(TrainingTypeModel type)
        {
            Type = type;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public TrainingType Type { get; private set; }
    }
}